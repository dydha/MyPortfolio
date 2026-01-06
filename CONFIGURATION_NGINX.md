# Configuration Nginx - Guide Complet

## 📍 Où Remplacer le Nom de Domaine ?

### Option 1 : Si vous avez un DOMAINE

Dans le fichier `/etc/nginx/sites-available/myportfolio`, remplacez :

```nginx
server_name your-domain.com www.your-domain.com;
```

Par votre vrai domaine, par exemple :

```nginx
server_name monportfolio.com www.monportfolio.com;
```

### Option 2 : Si vous utilisez uniquement l'IP du VPS

Remplacez par :

```nginx
server_name _;
```

Le `_` signifie "accepter toutes les requêtes", que ce soit par IP ou par domaine.

## 🔧 Configuration Complète à Utiliser

### Pour un VPS avec IP uniquement (Recommandé pour commencer)

```nginx
server {
    listen 80;
    server_name _;  # ← ICI : Accepte toutes les requêtes (IP ou domaine)
    
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

### Pour un VPS avec DOMAINE

```nginx
server {
    listen 80;
    server_name monportfolio.com www.monportfolio.com;  # ← ICI : Remplacez par votre domaine
    
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

## 📝 Étapes pour Configurer sur le VPS

### 1. Créer le fichier de configuration

```bash
sudo nano /etc/nginx/sites-available/myportfolio
```

### 2. Copier la configuration

- **Si vous avez un domaine** : Utilisez la configuration "Pour un VPS avec DOMAINE" ci-dessus
- **Si vous n'avez pas de domaine** : Utilisez la configuration "Pour un VPS avec IP uniquement" ci-dessus

### 3. Remplacer le nom de domaine (si nécessaire)

Dans l'éditeur nano :
- Appuyez sur `Ctrl+W` pour rechercher
- Tapez `your-domain.com` ou `server_name`
- Remplacez par votre domaine ou `_`

### 4. Sauvegarder

- `Ctrl+X` pour quitter
- `Y` pour confirmer
- `Enter` pour sauvegarder

### 5. Activer et tester

```bash
# Créer le lien symbolique
sudo ln -s /etc/nginx/sites-available/myportfolio /etc/nginx/sites-enabled/

# Tester la configuration
sudo nginx -t

# Si OK, recharger Nginx
sudo systemctl reload nginx
```

## 🎯 Exemples Concrets

### Exemple 1 : Domaine "portfolio.dydha.com"
```nginx
server_name portfolio.dydha.com www.portfolio.dydha.com;
```

### Exemple 2 : Domaine "mon-site.fr"
```nginx
server_name mon-site.fr www.mon-site.fr;
```

### Exemple 3 : Pas de domaine (IP uniquement)
```nginx
server_name _;
```

## ✅ Vérification

Après configuration, testez :

```bash
# Vérifier la configuration
sudo nginx -t

# Voir la configuration active
sudo nginx -T | grep server_name

# Tester depuis le VPS
curl -H "Host: votre-domaine.com" http://localhost
```

## 🔒 Configuration SSL (Plus tard)

Une fois que votre site fonctionne, vous pouvez ajouter SSL avec Let's Encrypt :

```bash
sudo apt install certbot python3-certbot-nginx -y
sudo certbot --nginx -d votre-domaine.com -d www.votre-domaine.com
```

Certbot modifiera automatiquement la configuration pour ajouter HTTPS.

