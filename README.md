# Gestion des champs d’oliviers

Application web ASP.NET Core MVC pour suivre les champs, les oliviers et leurs récoltes.

## Règle métier

Chaque olivier produit **une récolte par an**. L’application refuse un second enregistrement pour le même olivier et la même année.

## Fonctionnalités

- CRUD des champs, oliviers et récoltes
- Filtrage des récoltes par année et par champ
- Graphique d’évolution des productions par champ pour une période donnée

## Lancer l’application

Prérequis : [.NET SDK 10](https://dotnet.microsoft.com/download)

L’interface est disponible en français, anglais, arabe et chinois simplifié. Le choix de langue est conservé dans un cookie ; l’arabe active automatiquement l’affichage de droite à gauche.

```bash
dotnet restore
dotnet run --urls http://localhost:5088
```

Ouvrir http://localhost:5088

Les données d’exemple (3 champs, 13 oliviers de variétés locales et internationales et récoltes simulées de 2020 à 2026) sont créées automatiquement dans `oliviers.db` (SQLite) uniquement lorsque la base ne contient pas encore de champs. Les données existantes ne sont pas remplacées.
