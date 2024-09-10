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
- Touching the boss will damage the player.

## Attacks
Every attack will have a wind up period before actually attacking.

#### Vines that tracks the player position
- Vines protrude on player position.
- Constantly active on the room of the boss, deactivates attack when not in boss room.
- Vines spawn point will find the player position then when it decides to attack give it a delay of 2 seconds to display spawn point and then protrude. 
- Spike will stay for 2 seconds before disappearing
- occurs every 8 seconds (make this changeable, counted after mark spawn).
![[mark.png]]
![[mark2.png]]
![[mark 3.png]]
![[Mark4.png]]


#### 360 rotating bullet hell pattern
Shoots out projectile in a rotating pattern
Since i dont know what better way to convey the direction/movement of the projectiles. Imagine you're holding a water hose and then spinning, The boss is basically doing that but with projectiles.

![[Pattern 1.png]]


#### All direction projectiles

Shoots projectile to all directions all at once
![[pattern 2.png]]

##### Left & right facing projectiles

![[Activation zone.png]]
-Only activates when the player is in the activation zone.

![[Left and right.png]]
- Shoots 4 projectiles


#### Vines shooting up from floors (REDACTED)
- have the same mark and delay mechanics like "Vines that tracks the player position" attack
- Spikes stay for 1 second before disappearing
- have a mark delay of 5 seconds

Theres 2 phases on this attack:

1. Protrude spikes from all platform floors

![[spikes 1.png]]

2. then, protrude from all solid floors

![[spikes 2.png]]