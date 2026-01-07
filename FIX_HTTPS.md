# Fix HTTPS - Configuration SSL pour dydhampandou.com

## 🔍 Diagnostic Rapide

### 1. Vérifier si SSL est configuré sur le VPS

Connectez-vous au VPS et exécutez :

```bash
ssh root@VOTRE_IP_VPS

# Vérifier si Certbot est installé
which certbot

# Vérifier les certificats existants
sudo certbot certificates

# Vérifier la configuration Nginx
sudo nginx -T | grep -A 5 "dydhampandou.com"
```

### 2. Vérifier les logs Nginx

```bash
# Logs d'erreur
sudo tail -50 /var/log/nginx/error.log

# Logs d'accès
sudo tail -50 /var/log/nginx/access.log
```

## 🔧 Solution : Configurer SSL avec Let's Encrypt

### Étape 1 : Installer Certbot

```bash
sudo apt update
sudo apt install certbot python3-certbot-nginx -y
```

### Étape 2 : Configurer le domaine dans Nginx

Assurez-vous que votre configuration Nginx utilise le bon domaine :

```bash
sudo nano /etc/nginx/sites-available/myportfolio
```

La configuration doit être :

```nginx
server {
    listen 80;
    server_name dydhampandou.com www.dydhampandou.com;
    
    root /var/www/myportfolio;
    index index.html;

    # Compression gzip
    gzip on;
    gzip_vary on;
    gzip_min_length 1024;
    gzip_types text/plain text/css text/xml text/javascript application/javascript application/json;

    # Cache pour les assets statiques
    location ~* \.(jpg|jpeg|png|gif|ico|css|js|woff|woff2|ttf|svg)$ {
        expires 1y;
        add_header Cache-Control "public, immutable";
    }

    # Toutes les routes pointent vers index.html pour le routing Blazor
    location / {
        try_files $uri $uri/ /index.html;
    }

    # Sécurité
    server_tokens off;
    add_header X-Frame-Options "SAMEORIGIN" always;
    add_header X-Content-Type-Options "nosniff" always;
    add_header X-XSS-Protection "1; mode=block" always;
}
```

Sauvegardez et testez :

```bash
sudo nginx -t
sudo systemctl reload nginx
```

### Étape 3 : Obtenir le certificat SSL

```bash
sudo certbot --nginx -d dydhampandou.com -d www.dydhampandou.com
```

Certbot va :
- Demander votre email (pour les notifications de renouvellement)
- Demander si vous acceptez les termes
- Configurer automatiquement HTTPS
- Rediriger HTTP vers HTTPS

### Étape 4 : Vérifier la configuration automatique

Certbot modifie automatiquement votre fichier Nginx. Vérifiez :

```bash
sudo cat /etc/nginx/sites-available/myportfolio
```

Vous devriez voir deux blocs `server` :
- Un pour HTTP (port 80) qui redirige vers HTTPS
- Un pour HTTPS (port 443) avec votre site

### Étape 5 : Tester

```bash
# Vérifier que Nginx fonctionne
sudo nginx -t
sudo systemctl reload nginx

# Tester HTTPS depuis le VPS
curl -I https://dydhampandou.com
```

## 🔄 Si Certbot a déjà été exécuté

Si vous avez déjà configuré SSL mais que ça ne fonctionne pas :

### Vérifier le renouvellement automatique

```bash
# Tester le renouvellement
sudo certbot renew --dry-run

# Vérifier le statut
sudo systemctl status certbot.timer
```

### Vérifier le firewall

```bash
# Vérifier que les ports 80 et 443 sont ouverts
sudo ufw status
sudo netstat -tlnp | grep -E ':(80|443)'
```

## 🐛 Problèmes Courants

### Erreur : "Failed to obtain certificate"

**Causes possibles** :
1. Le domaine ne pointe pas vers le VPS
2. Le port 80 est bloqué par le firewall
3. Nginx n'est pas configuré correctement

**Solution** :
```bash
# Vérifier que le domaine pointe vers le VPS
nslookup dydhampandou.com

# Vérifier le firewall
sudo ufw allow 80/tcp
sudo ufw allow 443/tcp
sudo ufw reload
```

### Erreur : "Connection refused" sur HTTPS

**Solution** :
```bash
# Vérifier que Nginx écoute sur le port 443
sudo netstat -tlnp | grep :443

# Vérifier la configuration SSL dans Nginx
sudo nginx -T | grep -A 10 "listen 443"
```

### Le site fonctionne en HTTP mais pas en HTTPS

**Vérifier** :
1. Que le certificat existe : `sudo ls -la /etc/letsencrypt/live/dydhampandou.com/`
2. Que Nginx est configuré pour HTTPS
3. Que le port 443 est ouvert

## 📋 Checklist de Vérification

- [ ] Le domaine `dydhampandou.com` pointe vers l'IP du VPS
- [ ] Nginx est configuré avec `server_name dydhampandou.com`
- [ ] Certbot est installé
- [ ] Le certificat SSL est obtenu avec `certbot --nginx`
- [ ] Les ports 80 et 443 sont ouverts dans le firewall
- [ ] Nginx écoute sur les ports 80 et 443
- [ ] La configuration Nginx est valide (`sudo nginx -t`)

## 🚀 Commandes Rapides

```bash
# Configuration complète en une fois
sudo apt update
sudo apt install certbot python3-certbot-nginx -y
sudo certbot --nginx -d dydhampandou.com -d www.dydhampandou.com
sudo nginx -t
sudo systemctl reload nginx

# Vérifier que ça fonctionne
curl -I https://dydhampandou.com
```

## 📞 Vérification DNS

Assurez-vous que votre domaine pointe vers le VPS :

```bash
# Vérifier le DNS
nslookup dydhampandou.com
dig dydhampandou.com

# Vous devriez voir l'IP de votre VPS
```

