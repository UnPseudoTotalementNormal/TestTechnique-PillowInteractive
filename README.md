### Téléchargement de la build:  
[LIEN SWISSTRANSFER]  

## Controles:  
Click gauche : sélectionner un matelot.  
Click droit : affecter une tâche au matelot sélectionné.  

Les tâches apparaissent à un certain interval.

## Choix technique réalisé:  
J'ai utilisé une StateMachine pour le fonctionnement des IA des matelots, cela permet de facilement créer des comportements différents en fonction de l'état du personnage et garde le code propre.  
Les taches (TaskObject) sont des ScriptableObject pour garder les données qui seront ensuite mis sur des component (TaskComponent), ça permet de modifier rapidement les valeurs de chaque tâche, avoir la même tâche sur deux prefabs différentes, etc.  
Les responsabilités sont séparés entre les components afin de garder le code lisible et modulable.  

## Plugin utilisé:  
DOTween, afin de créer des animations simples rapidement sans complexifier le code
