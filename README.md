### Téléchargement de la build :  
[[LIEN SWISSTRANSFER](https://www.swisstransfer.com/d/056903a2-7149-4ad1-bf11-8d5258705de0)]  

## Explications :  
Les tâches apparaissent à un certain intervalle. Lorsqu'une tâche doit être effectuée, une icône apparaît au-dessus.
Pour effectuer une tâche, vous devez cliquer (gauche) sur un matelot pour le sélectionner, puis cliquer (droit) sur la tâche disponible.  
Lorsque les matelots ont effectué des tâches et sont fatigués, ils iront se reposer puis seront à nouveau disponibles.  

## Contrôles :  
Clic gauche : sélectionner un matelot.  
Clic droit : affecter une tâche au matelot sélectionné.  

## Choix technique réalisé :  
J'ai utilisé une StateMachine pour le fonctionnement des IA des matelots. Cela permet de facilement créer des comportements différents en fonction de l'état du personnage et garde le code propre.  
Les tâches (TaskObject) sont des ScriptableObject pour garder les données, qui seront ensuite mises sur des components (TaskComponent). Ça permet de modifier rapidement les valeurs de chaque tâche, d’avoir la même tâche sur deux prefabs différents, etc.  
Les responsabilités sont séparées entre les components afin de garder le code lisible et modulable.  

## Plugin utilisé :  
DOTween, afin de créer des animations simples rapidement sans complexifier le code.
