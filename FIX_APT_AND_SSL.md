# Fix Problèmes Apt et Configuration SSL

## 🔴 Problème 1 : Erreur Apt (Microsoft packages)

### Solution Rapide

```bash
# Vérifier les sources problématiques
sudo ls -la /etc/apt/sources.list.d/ | grep microsoft

# Voir le contenu des fichiers Microsoft
sudo cat /etc/apt/sources.list.d/microsoft-prod.list
sudo cat /etc/apt/sources.list.d/microsoft-prod.list.save

# Solution : Supprimer ou corriger les fichiers en double
sudo rm /etc/apt/sources.list.d/microsoft-prod.list.save 2>/dev/null || true

# Ou désactiver temporairement Microsoft sources
sudo mv /etc/apt/sources.list.d/microsoft-prod.list /etc/apt/sources.list.d/microsoft-prod.list.disabled

# Mettre à jour
sudo apt update
```

### Solution Alternative (si la première ne fonctionne pas)

```bash
# Voir tous les fichiers sources
sudo ls -la /etc/apt/sources.list.d/

# Supprimer les fichiers Microsoft en double
sudo find /etc/apt/sources.list.d/ -name "*microsoft*" -type f

# Supprimer les fichiers .save ou .bak
sudo rm /etc/apt/sources.list.d/*.save 2>/dev/null
sudo rm /etc/apt/sources.list.d/*.bak 2>/dev/null

# Mettre à jour
sudo apt update
```

## 🔴 Problème 2 : Nginx n'écoute pas sur le port 443

Cela signifie que SSL n'est pas configuré. Voici comment le configurer :

### Étape 1 : Corriger le problème Apt d'abord

```bash
# Supprimer les fichiers sources en conflit
sudo rm -f /etc/apt/sources.list.d/microsoft-prod.list.save
sudo rm -f /etc/apt/sources.list.d/*.save

# Mettre à jour
sudo apt update
```

### Étape 2 : Installer Certbot

```bash
sudo apt install certbot python3-certbot-nginx -y
```

Si ça ne fonctionne toujours pas :

```bash
# Installer depuis le repository Ubuntu standard
sudo apt install snapd -y
sudo snap install core; sudo snap refresh core
sudo snap install --classic certbot
sudo ln -s /snap/bin/certbot /usr/bin/certbot
```

### Étape 3 : Vérifier la configuration Nginx actuelle

```bash
# Voir la configuration complète
sudo cat /etc/nginx/sites-available/myportfolio

# Vérifier qu'elle contient dydhampandou.com (sans www aussi)
sudo grep -i "dydhampandou" /etc/nginx/sites-available/myportfolio
```

### Étape 4 : Corriger la configuration Nginx

```bash
sudo nano /etc/nginx/sites-available/myportfolio
```

Assurez-vous que la configuration contient **les deux** domaines :

```nginx
server {
    listen 80;
    server_name dydhampandou.com www.dydhampandou.com;  # ← Les deux domaines !
    
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

### Étape 5 : Obtenir le certificat SSL

```bash
# Avec apt certbot
sudo certbot --nginx -d dydhampandou.com -d www.dydhampandou.com

# OU avec snap certbot
sudo /snap/bin/certbot --nginx -d dydhampandou.com -d www.dydhampandou.com
```

### Étape 6 : Vérifier le firewall

```bash
# Vérifier le statut
sudo ufw status

# Ouvrir les ports si nécessaire
sudo ufw allow 80/tcp
sudo ufw allow 443/tcp
sudo ufw reload
```

## 🔍 Vérifications

### Vérifier que SSL est configuré

```bash
# Vérifier les certificats
sudo certbot certificates

# Vérifier que Nginx écoute sur 443
sudo netstat -tlnp | grep :443

# Vérifier la configuration HTTPS
sudo nginx -T | grep -A 15 "listen 443"
```

### Tester HTTPS

```bash
# Depuis le VPS
curl -I https://dydhampandou.com

# Depuis votre machine
curl -I https://dydhampandou.com
```

## 🐛 Note sur les erreurs dans les logs

Les erreurs pour `vault.dydhampanou.com` sont pour un autre service. Vous pouvez les ignorer ou les corriger séparément.

## 📋 Checklist Complète

- [ ] Problème apt résolu (`sudo apt update` fonctionne)
- [ ] Certbot installé
- [ ] Configuration Nginx contient `dydhampandou.com` ET `www.dydhampandou.com`
- [ ] Certificat SSL obtenu avec `certbot --nginx`
- [ ] Ports 80 et 443 ouverts dans le firewall
- [ ] Nginx écoute sur le port 443
- [ ] HTTPS fonctionne : `curl -I https://dydhampandou.com`

