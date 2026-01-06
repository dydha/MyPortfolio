# Guide de Test du Déploiement

## 🌐 URLs pour Tester

Une fois le déploiement terminé, vous pouvez accéder à votre site via :

### Si vous avez un domaine configuré :
```
http://votre-domaine.com
https://votre-domaine.com (si SSL est configuré)
```

### Si vous utilisez uniquement l'IP du VPS :
```
http://VOTRE_IP_VPS
```

**Exemple** : Si votre IP est `123.456.789.012`, l'URL sera :
```
http://123.456.789.012
```

## ✅ Checklist de Vérification

### 1. Vérifier que le déploiement a réussi

**Sur GitHub Actions** :
- Allez dans l'onglet **Actions** de votre repository
- Vérifiez que le dernier workflow est marqué en vert (✅)
- Cliquez sur le workflow pour voir les détails
- Vérifiez que toutes les étapes sont réussies

### 2. Vérifier que les fichiers sont sur le VPS

Connectez-vous au VPS :
```bash
ssh root@VOTRE_IP_VPS
```

Vérifiez que les fichiers sont présents :
```bash
ls -la /var/www/myportfolio
```

Vous devriez voir des fichiers comme :
- `index.html`
- `_framework/`
- `css/`
- `Images/`
- etc.

### 3. Vérifier que Nginx fonctionne

```bash
# Vérifier le statut de Nginx
sudo systemctl status nginx

# Vérifier la configuration
sudo nginx -t

# Voir les logs en temps réel
sudo tail -f /var/log/nginx/error.log
```

### 4. Tester l'accès au site

**Dans votre navigateur** :
1. Ouvrez `http://VOTRE_IP_VPS` ou `http://votre-domaine.com`
2. Vous devriez voir votre portfolio Blazor
3. Testez la navigation entre les pages
4. Testez le changement de thème (dark/light mode)

**Avec curl (depuis le terminal)** :
```bash
curl -I http://VOTRE_IP_VPS
```

Vous devriez voir :
```
HTTP/1.1 200 OK
Server: nginx
...
```

### 5. Vérifier les routes Blazor

Testez les différentes routes de votre application :
- `/` - Page d'accueil
- `/projects` - Liste des projets
- `/skills` - Compétences
- `/career` - Carrière
- `/contact` - Contact
- `/cv` - CV

Toutes ces routes devraient fonctionner grâce à la configuration Nginx avec `try_files $uri $uri/ /index.html;`

## 🔧 Commandes Utiles pour le Debugging

### Voir les logs Nginx en temps réel
```bash
ssh root@VOTRE_IP_VPS
sudo tail -f /var/log/nginx/access.log
sudo tail -f /var/log/nginx/error.log
```

### Vérifier les permissions
```bash
ssh root@VOTRE_IP_VPS
ls -la /var/www/myportfolio
```

### Redémarrer Nginx manuellement
```bash
ssh root@VOTRE_IP_VPS
sudo systemctl restart nginx
```

### Vérifier que le port 80 est ouvert
```bash
ssh root@VOTRE_IP_VPS
sudo netstat -tlnp | grep :80
```

## 🐛 Problèmes Courants

### Le site ne se charge pas

1. **Vérifiez que Nginx est actif** :
```bash
sudo systemctl status nginx
```

2. **Vérifiez les logs d'erreur** :
```bash
sudo tail -50 /var/log/nginx/error.log
```

3. **Vérifiez que les fichiers existent** :
```bash
ls -la /var/www/myportfolio
```

### Erreur 404 sur les routes Blazor

Assurez-vous que la configuration Nginx contient :
```nginx
location / {
    try_files $uri $uri/ /index.html;
}
```

### Erreur 403 Forbidden

Vérifiez les permissions :
```bash
sudo chown -R www-data:www-data /var/www/myportfolio
sudo chmod -R 755 /var/www/myportfolio
```

### Le site charge mais les assets ne se chargent pas

Vérifiez que tous les fichiers sont bien déployés :
```bash
ls -la /var/www/myportfolio/_framework/
ls -la /var/www/myportfolio/css/
ls -la /var/www/myportfolio/Images/
```

## 📱 Test depuis différents appareils

Testez votre site depuis :
- ✅ Ordinateur (Chrome, Firefox, Edge)
- ✅ Mobile (navigateur mobile)
- ✅ Tablette

## 🔒 Test SSL (si configuré)

Si vous avez configuré SSL avec Let's Encrypt :
```bash
# Tester le certificat SSL
curl -I https://votre-domaine.com

# Vérifier la date d'expiration
sudo certbot certificates
```

## 📊 Monitoring

Pour surveiller les accès à votre site :
```bash
# Voir les dernières requêtes
sudo tail -f /var/log/nginx/access.log

# Compter les requêtes par IP
sudo awk '{print $1}' /var/log/nginx/access.log | sort | uniq -c | sort -nr | head -10
```

## ✅ Test Final

Une fois que tout fonctionne, testez :
- [ ] Le site se charge correctement
- [ ] Toutes les pages sont accessibles
- [ ] Les images se chargent
- [ ] Le dark mode fonctionne
- [ ] La navigation fonctionne
- [ ] Les liens externes (GitHub, LinkedIn) fonctionnent
- [ ] Le téléchargement du CV fonctionne

---

**Félicitations ! 🎉** Si tous ces tests passent, votre déploiement est réussi !

