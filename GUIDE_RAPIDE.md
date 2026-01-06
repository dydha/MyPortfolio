# Guide Rapide - Déploiement MyPortfolio

## 📋 Checklist de Déploiement

### Étape 1 : Configuration Initiale du VPS (à faire UNE SEULE FOIS)

1. **Connectez-vous à votre VPS Ubuntu chez Hostinger**
   ```bash
   ssh root@VOTRE_IP_VPS
   ```

2. **Exécutez le script d'installation** (copiez-le d'abord sur le VPS)
   
   Sur votre machine locale :
   ```bash
   scp scripts/setup-vps.sh root@VOTRE_IP_VPS:/tmp/
   ```
   
   Puis sur le VPS :
   ```bash
   bash /tmp/setup-vps.sh
   ```

3. **Configurez Nginx**
   
   Copiez le fichier de configuration :
   ```bash
   # Depuis votre machine locale
   scp nginx/myportfolio.conf root@VOTRE_IP_VPS:/tmp/myportfolio.conf
   ```
   
   Sur le VPS :
   ```bash
   sudo mv /tmp/myportfolio.conf /etc/nginx/sites-available/myportfolio
   sudo nano /etc/nginx/sites-available/myportfolio
   # Remplacez "your-domain.com" par votre domaine réel
   sudo ln -s /etc/nginx/sites-available/myportfolio /etc/nginx/sites-enabled/
   sudo nginx -t
   sudo systemctl reload nginx
   ```

### Étape 2 : Générer une Clé SSH pour GitHub Actions

**Sur Windows (PowerShell)** :

```powershell
# 1. Générer la clé SSH
ssh-keygen -t ed25519 -C "github-actions-deploy" -f $env:USERPROFILE\.ssh\github_actions_deploy

# 2. Copier la clé publique sur le VPS
type $env:USERPROFILE\.ssh\github_actions_deploy.pub | ssh root@VOTRE_IP_VPS "mkdir -p ~/.ssh && cat >> ~/.ssh/authorized_keys"

# 3. Afficher la clé privée (vous en aurez besoin pour GitHub)
Get-Content $env:USERPROFILE\.ssh\github_actions_deploy
```

**Sur Linux/Mac** :

```bash
# 1. Générer la clé SSH
ssh-keygen -t ed25519 -C "github-actions-deploy" -f ~/.ssh/github_actions_deploy

# 2. Copier la clé publique sur le VPS
ssh-copy-id -i ~/.ssh/github_actions_deploy.pub root@VOTRE_IP_VPS

# 3. Afficher la clé privée
cat ~/.ssh/github_actions_deploy
```

### Étape 3 : Configurer les Secrets GitHub

1. **Allez sur votre repository GitHub**
2. **Cliquez sur Settings** (en haut du repository)
3. **Dans le menu de gauche, cliquez sur "Secrets and variables" > "Actions"**
4. **Cliquez sur "New repository secret"** et ajoutez ces 4 secrets :

   | Nom du Secret | Valeur | Exemple |
   |--------------|--------|---------|
   | `VPS_HOST` | L'IP ou domaine de votre VPS | `123.456.789.012` ou `vps.mondomaine.com` |
   | `VPS_USER` | Utilisateur SSH | `root` |
   | `VPS_SSH_KEY` | Le contenu complet de la clé privée (de l'étape 2) | `-----BEGIN OPENSSH PRIVATE KEY-----...` |
   | `VPS_PORT` | Port SSH (optionnel) | `22` |

### Étape 4 : Vérifier que votre branche s'appelle "main"

```bash
# Vérifiez le nom de votre branche actuelle
git branch

# Si votre branche s'appelle "master" au lieu de "main", soit :
# Option A : Renommez-la en "main"
git branch -M main

# Option B : Modifiez le workflow pour utiliser "master"
# (éditez .github/workflows/deploy.yml ligne 6)
```

### Étape 5 : Premier Déploiement

**Option A : Déploiement automatique via GitHub**
```bash
# Commitez et poussez vos changements
git add .
git commit -m "Configuration du déploiement"
git push origin main
```

Le workflow se déclenchera automatiquement. Vérifiez-le dans l'onglet **Actions** de GitHub.

**Option B : Déploiement manuel (pour tester)**
```bash
# Configurez les variables d'environnement
$env:VPS_HOST="VOTRE_IP_VPS"
$env:VPS_USER="root"
$env:VPS_PORT="22"

# Exécutez le script (dans Git Bash ou WSL)
bash deploy.sh
```

### Étape 6 : Vérifier le Déploiement

1. Visitez votre site : `http://VOTRE_IP_VPS` ou `http://votre-domaine.com`
2. Si ça ne fonctionne pas, vérifiez les logs :
   ```bash
   ssh root@VOTRE_IP_VPS
   sudo tail -f /var/log/nginx/error.log
   ```

## 🔧 Configuration SSL (Optionnel mais Recommandé)

Une fois que tout fonctionne, configurez HTTPS :

```bash
ssh root@VOTRE_IP_VPS
sudo apt install certbot python3-certbot-nginx -y
sudo certbot --nginx -d votre-domaine.com -d www.votre-domaine.com
```

## ❓ Dépannage Rapide

### Le workflow GitHub Actions échoue
- Vérifiez que tous les secrets sont bien configurés
- Vérifiez que la clé SSH fonctionne : `ssh -i ~/.ssh/github_actions_deploy root@VOTRE_IP_VPS`
- Consultez les logs dans l'onglet Actions de GitHub

### Le site ne se charge pas
- Vérifiez que Nginx est actif : `sudo systemctl status nginx`
- Vérifiez les permissions : `ls -la /var/www/myportfolio`
- Vérifiez la config Nginx : `sudo nginx -t`

### Erreur 404 sur les routes Blazor
- Assurez-vous que la config Nginx a bien `try_files $uri $uri/ /index.html;`

## 📞 Besoin d'Aide ?

Consultez le fichier `DEPLOYMENT.md` pour plus de détails.

