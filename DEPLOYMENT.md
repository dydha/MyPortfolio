# Guide de Déploiement - MyPortfolio

Ce guide explique comment déployer l'application Blazor WebAssembly sur un VPS Ubuntu chez Hostinger.

## Prérequis

- Un VPS Ubuntu chez Hostinger
- Accès SSH au VPS
- Un domaine pointant vers l'IP du VPS (optionnel mais recommandé)
- Un compte GitHub avec le repository de ce projet

## Configuration Initiale du VPS

### 1. Connexion au VPS

```bash
ssh root@your-vps-ip
```

### 2. Mise à jour du système

```bash
sudo apt update && sudo apt upgrade -y
```

### 3. Installation de Nginx

```bash
sudo apt install nginx -y
sudo systemctl enable nginx
sudo systemctl start nginx
```

### 4. Création du répertoire de déploiement

```bash
sudo mkdir -p /var/www/myportfolio
sudo chown -R $USER:$USER /var/www/myportfolio
sudo chmod -R 755 /var/www/myportfolio
```

### 5. Configuration Nginx

Copiez le fichier `nginx/myportfolio.conf` sur votre VPS et adaptez-le :

```bash
# Sur votre machine locale
scp nginx/myportfolio.conf root@your-vps-ip:/tmp/myportfolio.conf

# Sur le VPS
sudo mv /tmp/myportfolio.conf /etc/nginx/sites-available/myportfolio
sudo ln -s /etc/nginx/sites-available/myportfolio /etc/nginx/sites-enabled/
```

**Important** : Modifiez `your-domain.com` dans le fichier de configuration par votre domaine réel.

```bash
sudo nano /etc/nginx/sites-available/myportfolio
```

Testez la configuration :

```bash
sudo nginx -t
```

Si tout est correct, rechargez Nginx :

```bash
sudo systemctl reload nginx
```

### 6. Configuration SSL avec Let's Encrypt (Recommandé)

```bash
sudo apt install certbot python3-certbot-nginx -y
sudo certbot --nginx -d your-domain.com -d www.your-domain.com
```

Le certificat sera renouvelé automatiquement.

## Configuration GitHub Actions

### 1. Secrets GitHub

Dans votre repository GitHub, allez dans **Settings > Secrets and variables > Actions** et ajoutez les secrets suivants :

- `VPS_HOST` : L'adresse IP ou le domaine de votre VPS
- `VPS_USER` : Le nom d'utilisateur SSH (généralement `root`)
- `VPS_SSH_KEY` : Votre clé privée SSH
- `VPS_PORT` : Le port SSH (par défaut 22)

### 2. Génération d'une clé SSH pour le déploiement

Sur votre machine locale :

```bash
ssh-keygen -t ed25519 -C "github-actions-deploy" -f ~/.ssh/github_actions_deploy
```

Copiez la clé publique sur le VPS :

```bash
ssh-copy-id -i ~/.ssh/github_actions_deploy.pub root@your-vps-ip
```

Copiez le contenu de la clé privée :

```bash
cat ~/.ssh/github_actions_deploy
```

Collez ce contenu dans le secret `VPS_SSH_KEY` sur GitHub.

## Déploiement Automatique

Une fois configuré, le déploiement se fait automatiquement à chaque push sur la branche `main`.

Pour déclencher un déploiement manuel :
1. Allez dans l'onglet **Actions** de votre repository GitHub
2. Sélectionnez le workflow **Deploy to VPS**
3. Cliquez sur **Run workflow**

## Déploiement Manuel

Si vous préférez déployer manuellement, utilisez le script `deploy.sh` :

### 1. Configuration des variables d'environnement

```bash
export VPS_HOST="your-vps-ip"
export VPS_USER="root"
export VPS_PORT="22"
```

### 2. Exécution du script

Sur Linux/Mac :

```bash
chmod +x deploy.sh
./deploy.sh
```

Sur Windows (avec Git Bash ou WSL) :

```bash
bash deploy.sh
```

## Vérification du Déploiement

1. Visitez votre domaine ou l'IP du VPS dans un navigateur
2. Vérifiez les logs Nginx en cas de problème :

```bash
sudo tail -f /var/log/nginx/error.log
sudo tail -f /var/log/nginx/access.log
```

## Dépannage

### L'application ne se charge pas

1. Vérifiez que Nginx est actif : `sudo systemctl status nginx`
2. Vérifiez les permissions : `ls -la /var/www/myportfolio`
3. Vérifiez la configuration : `sudo nginx -t`

### Erreur 404 sur les routes Blazor

Assurez-vous que la configuration Nginx inclut `try_files $uri $uri/ /index.html;`

### Problèmes de permissions

```bash
sudo chown -R www-data:www-data /var/www/myportfolio
sudo chmod -R 755 /var/www/myportfolio
```

## Structure des Fichiers

```
MyPortfolio/
├── .github/
│   └── workflows/
│       └── deploy.yml          # Workflow GitHub Actions
├── nginx/
│   └── myportfolio.conf        # Configuration Nginx
├── deploy.sh                   # Script de déploiement manuel
└── DEPLOYMENT.md               # Ce fichier
```

## Support

Pour toute question ou problème, consultez :
- [Documentation Nginx](https://nginx.org/en/docs/)
- [Documentation GitHub Actions](https://docs.github.com/en/actions)
- [Documentation Blazor](https://learn.microsoft.com/en-us/aspnet/core/blazor/)

