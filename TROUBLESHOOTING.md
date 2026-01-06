# Guide de Dépannage - Déploiement

## 🔴 Erreur : "ssh: no key found" ou "unable to authenticate"

Cette erreur signifie que votre clé SSH dans les secrets GitHub n'est pas correctement formatée.

### Solution 1 : Vérifier le format de la clé SSH

La clé privée SSH doit être au format **OpenSSH** ou **PEM**. Voici comment vérifier et corriger :

#### Sur Windows (PowerShell)

1. **Générez une nouvelle clé SSH** (si nécessaire) :
```powershell
ssh-keygen -t ed25519 -C "github-actions-deploy" -f $env:USERPROFILE\.ssh\github_actions_deploy
```

2. **Affichez la clé privée** :
```powershell
Get-Content $env:USERPROFILE\.ssh\github_actions_deploy
```

3. **Vérifiez le format** :
   - La clé doit commencer par `-----BEGIN OPENSSH PRIVATE KEY-----` ou `-----BEGIN RSA PRIVATE KEY-----`
   - La clé doit se terminer par `-----END OPENSSH PRIVATE KEY-----` ou `-----END RSA PRIVATE KEY-----`
   - **Copiez TOUT le contenu**, y compris les lignes de début et de fin

4. **Copiez la clé publique sur le VPS** :
```powershell
type $env:USERPROFILE\.ssh\github_actions_deploy.pub | ssh root@VOTRE_IP_VPS "mkdir -p ~/.ssh && cat >> ~/.ssh/authorized_keys"
```

5. **Dans GitHub** :
   - Allez dans **Settings > Secrets and variables > Actions**
   - Modifiez le secret `VPS_SSH_KEY`
   - Collez **TOUT** le contenu de la clé privée (avec les lignes `-----BEGIN...` et `-----END...`)
   - **Important** : Ne laissez pas d'espaces avant ou après, et gardez les sauts de ligne

### Solution 2 : Vérifier les permissions sur le VPS

Connectez-vous au VPS et vérifiez les permissions :

```bash
ssh root@VOTRE_IP_VPS

# Vérifiez les permissions du répertoire .ssh
ls -la ~/.ssh

# Les permissions doivent être :
# - ~/.ssh : 700 (drwx------)
# - ~/.ssh/authorized_keys : 600 (-rw-------)

# Si ce n'est pas le cas, corrigez :
chmod 700 ~/.ssh
chmod 600 ~/.ssh/authorized_keys
```

### Solution 3 : Tester la connexion manuellement

Testez la connexion SSH avec la clé :

```powershell
# Sur Windows
ssh -i $env:USERPROFILE\.ssh\github_actions_deploy root@VOTRE_IP_VPS
```

Si cela fonctionne, la clé est correcte. Si cela ne fonctionne pas, vérifiez :
- Que la clé publique est bien dans `~/.ssh/authorized_keys` sur le VPS
- Que les permissions sont correctes
- Que le serveur SSH accepte les connexions par clé

## 🔴 Erreur : "Permission denied"

### Vérifier les secrets GitHub

Assurez-vous que tous les secrets sont correctement configurés :

| Secret | Format attendu | Exemple |
|--------|---------------|---------|
| `VPS_HOST` | IP ou domaine | `123.456.789.012` |
| `VPS_USER` | Nom d'utilisateur | `root` |
| `VPS_SSH_KEY` | Clé privée complète | `-----BEGIN OPENSSH...` |
| `VPS_PORT` | Port SSH (optionnel) | `22` |

### Vérifier que la clé publique est sur le VPS

```bash
# Sur le VPS
cat ~/.ssh/authorized_keys
```

Vous devriez voir votre clé publique (commence par `ssh-ed25519` ou `ssh-rsa`).

## 🔴 Erreur : "No such file or directory" lors du déploiement

### Vérifier que le répertoire existe sur le VPS

```bash
ssh root@VOTRE_IP_VPS
sudo mkdir -p /var/www/myportfolio
sudo chown -R $USER:$USER /var/www/myportfolio
```

## 🔴 Erreur : Nginx ne redémarre pas

### Vérifier la configuration Nginx

```bash
ssh root@VOTRE_IP_VPS
sudo nginx -t
```

Si la configuration est incorrecte, corrigez-la :

```bash
sudo nano /etc/nginx/sites-available/myportfolio
sudo nginx -t
sudo systemctl reload nginx
```

## 📋 Checklist de Vérification

Avant de relancer le workflow, vérifiez :

- [ ] La clé SSH privée est correctement formatée dans GitHub Secrets
- [ ] La clé publique SSH est dans `~/.ssh/authorized_keys` sur le VPS
- [ ] Les permissions SSH sont correctes (700 pour .ssh, 600 pour authorized_keys)
- [ ] La connexion SSH fonctionne manuellement avec la clé
- [ ] Le répertoire `/var/www/myportfolio` existe sur le VPS
- [ ] Nginx est installé et configuré
- [ ] Tous les secrets GitHub sont correctement configurés

## 🆘 Besoin d'aide supplémentaire ?

Si le problème persiste :
1. Vérifiez les logs complets dans l'onglet **Actions** de GitHub
2. Testez la connexion SSH manuellement
3. Vérifiez les logs du VPS : `sudo tail -f /var/log/auth.log`

