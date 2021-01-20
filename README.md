# Boids-Simulation
This project simulates the flocking behavior of birds. It uses Unity3d engine to implement the Boids algorithm.

Boids algorithm is an emergent-behavior approach that follows three main rules:

1) **Cohesion** force is applied to local flockmates to steer towards their average position (center of mass) => pulling them toward each other.
2) **Separation** force is applied to local flockmates to avoid crowding local flockmates => pushing each other away.
3) **Alignment**:

| Cohesion | Separation | Alignment |
| :-----: | :-------: | :-------: |
| <img src="docs/Cohesion.gif" height="200" /> | <img src="docs/Separation.gif" height="200" /> | <img src="docs/Alignment.gif" height="200" /> |
