NeuroScoutingGame
=================

The NeuroScouting game project

This is the NeuroScouting Technical Interview Prototype Game.

In this game, the player has to hit the correct target Pool Ball as fast as possible into the hole. 
The player does this by pressing space as soon as the player sees the ball.

The player first selects how many trials they would like to play.

In each trial, a Target Ball is displayed for 2 seconds. Then 6 pool balls will roll across the pool table.
If the pool ball being rolled matches the Target Ball, the player must press Space as soon as they can to
hit it, and have it go in. This is repeated for as many trials as the player chose. In some cases, the target
ball may never appear, or could appear multiple times in a single trial.

A results screen with the scores will be displayed after all the trials.

The score is based on:
  -The average reaction time (+100 if perfect)
  -The number of correct hits (+100 each)
  -The number of incorrect hits (-100 each)
  -The ratio of correct to incorrect hits (+100 if 100%)
  -The number of missed target hits (-100 each)
  -The number of hits that actually went in (+100 each)
  
This game is made up of one scene and multiple object. Most of the core code lies in the Main Camera 
object, that handles the trial player choosing, the Target Ball displaying before each trial, and
initializing the game. Then, the main character activates a Ball Generator object that generates
the Pool balls every 3 seconds for 2 seconds each. The ball generator stops after 6 balls, unless
the main camera scripts call the ball generator to create another 6 balls. Almost all scripts return
data to the Main Camera object scripts. All the objects are connected by referencing the instance of
each other stored in a variable.

Lastly, The GUI text and buttons has been made so that they stay relative to the screen resolution. During
programming, there was the error of resizing causing the text to get cut off. Now the game can be played
in any resolution.

Currently, there is no way to reset the game except to close and re-open the game.
