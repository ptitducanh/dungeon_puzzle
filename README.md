# dungeon_puzzle

This project uses Component Based Design for reusability. 

## Architecture

### Entity 
An Entity is just a simple component contains other components. 
Entity controls the life-cycle of all other components. This allows us to control the Update interval of each components. 
Entity also acts as an entry to access all the component without calling GetComponent function. 

### Components
#### Data Components
Data Component contains the data of an entity for easy access from other components
#### Behavioral Component 
Behavioral Component contain funcions of an Entity. Each Behavioral Component should be as simple as possible to maximize reusability.
