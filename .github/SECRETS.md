# Configuration des Secrets GitHub Actions

Ce fichier documente les secrets nécessaires pour le déploiement automatique via GitHub Actions.

## Secrets Requis

Ajoutez ces secrets dans **Settings > Secrets and variables > Actions** de votre repository GitHub :

### VPS_HOST
- **Description** : L'adresse IP ou le domaine de votre VPS
- **Exemple** : `123.456.789.012` ou `vps.example.com`
- **Obligatoire** : Oui

### VPS_USER
- **Description** : Le nom d'utilisateur SSH pour se connecter au VPS
- **Exemple** : `root` ou `ubuntu`
- **Obligatoire** : Oui
- **Par défaut** : `root`

### VPS_SSH_KEY
- **Description** : La clé privée SSH pour l'authentification
- **Format** : Contenu complet de la clé privée (commence par `-----BEGIN OPENSSH PRIVATE KEY-----`)
- **Obligatoire** : Oui

### VPS_PORT
- **Description** : Le port SSH du VPS
- **Exemple** : `22` ou `2222`
- **Obligatoire** : Non
- **Par défaut** : `22`

## Génération de la Clé SSH

### Sur Linux/Mac

```bash
# Générer une nouvelle clé SSH
ssh-keygen -t ed25519 -C "github-actions-deploy" -f ~/.ssh/github_actions_deploy

# Copier la clé publique sur le VPS
ssh-copy-id -i ~/.ssh/github_actions_deploy.pub root@your-vps-ip

# Afficher la clé privée pour la copier dans GitHub Secrets
cat ~/.ssh/github_actions_deploy
```

### Sur Windows (PowerShell)

```powershell
# Générer une nouvelle clé SSH
ssh-keygen -t ed25519 -C "github-actions-deploy" -f $env:USERPROFILE\.ssh\github_actions_deploy

# Copier la clé publique sur le VPS (remplacez your-vps-ip)
type $env:USERPROFILE\.ssh\github_actions_deploy.pub | ssh root@your-vps-ip "cat >> ~/.ssh/authorized_keys"

# Afficher la clé privée pour la copier dans GitHub Secrets
Get-Content $env:USERPROFILE\.ssh\github_actions_deploy
```

## Vérification de la Connexion

Testez la connexion SSH avant de configurer GitHub Actions :

```bash
ssh -i ~/.ssh/github_actions_deploy root@your-vps-ip
```

Si la connexion fonctionne sans mot de passe, la configuration est correcte.

## Sécurité

⚠️ **Important** :
- Ne partagez jamais votre clé privée SSH publiquement
- Ne commitez jamais la clé privée dans le repository Git
- Utilisez une clé SSH dédiée uniquement pour le déploiement
- Limitez les permissions de la clé SSH sur le VPS si possible

## Dépannage

### Erreur "Permission denied (publickey)"
- Vérifiez que la clé publique est bien dans `~/.ssh/authorized_keys` sur le VPS
- Vérifiez les permissions : `chmod 600 ~/.ssh/authorized_keys` et `chmod 700 ~/.ssh`

### Erreur "Host key verification failed"
- Ajoutez l'option `-o StrictHostKeyChecking=no` dans la configuration SSH (non recommandé en production)
- Ou ajoutez la clé du VPS à `~/.ssh/known_hosts` manuellement
