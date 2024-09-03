## Level Layout for the boss

![[Pasted image 20240903211922.png]]
HP: 90
## Notes
- Battle will **ALWAYS** start at day, to make sure of this before the boss battle we'll block the entrance at night so they can only progress when its daytime.
- There's 2 locked gates on the left and right part of the room which leads to trial rooms

## Mechanics
- At 30/60 remaining hp, change time to night, and become invulnerable.
- While invulnerable the boss will still attack the player.
- The gates on the left and right of the room opens at night.
- At 30 hp, the right gate is opened. 
- At 60 hp, the left gate is opened.
- Trial rooms will contain a platforming section that needs specific weapons to complete.
- At the end of the room there will be a bench to turn the time back to day.

## Attacks
Every attack will have a wind up animation before unleashing it.

##### Vines that tracks the player position
- Vines protrude on player position.
- Constantly active on the room of the boss, deactivates attack when not in boss room.
- Vines spawn point will constantly tracks the player then when it decides to attack give it a delay of 1 seconds to display spawn point and then protrude. 
- occurs every 6 seconds (make this changeable, exclude delay times).

##### 360 rotating bullet hell pattern
Shoots out projectile in a rotating pattern
![[Pattern 1.png]]
##### All direction projectiles
Shoots projectile in all directions all at once
![[pattern 2.png]]

##### Left/right facing projectiles
- Only activates when the player is 6 blocks away from the boss
- direction are decided based on where the player is (if the players on the left then the boss will shoot left)
- Shoots 4 projectiles with 4 height.
- for example, if the coordinates for the lowest leftmost tile is (1,1) then projectiles will be shot from (13, 7) (13, 8) (13, 9) (13, 10) to the left and (15, 7-10) to the right.


##### Vines shooting up from floors
theres 2 phases on this attack:
1. Protrude spikes from all platform floors
2. then, protrude from all solid floors

