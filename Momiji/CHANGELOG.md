## 1.1.6

bugfixing+new art

* fixed a bug where damaging spellcards would not be correctly read as Offensive Intentions.
* fixed a bug preventing MomijiB's basic attack(Shield Bash) from being copied, be it by cards or by exhibits.
* Added new artwork by the artist Radal on the Touhou LBoL Discord for the cards Exploit Openings and Traning Record

## 1.1.5

Minor bugfixing+localization changes

* finally fixed an error that would cause Training Record to softlock the game if it did not find a viable upgrade target.
* updated Japanese localization
* Smell of Death should now properly be tracked as a debuff, and subject to the same mechanics as a debuff(status cleansing/amulet)
* Fixed an error with Lookout's Intuition and Defensive strike that caused them to be affected by Frail.
* Fixed an error with the Status Effects granted by Circular Pacing, Destabilize, Stand Watch, and Woodland Sentry that caused them to be affected by Frail(note: the block granted by playing Stand Watch will still be affected by frail. The passive block gained each turn will not)

## 1.1.4

Fixing bugs

* fixed an error in the code for Shield Counter that would cause it to infinitely loop with Dance of Empty Masks
* Updated the Japanese Localization to match the English
* fixed an error in the description of Raging Waterfall
* Fixed Follow Up to now correctly grant mana when a targeted enemy with Vulnerable dies because of it.
* Fixed several small localization errors for both cards and status effects
* This changelog should now directly show up on the Thunderstore page

## 1.1.3

Bug Patching + Updating Localization

* Attempted a fix for the bug that causes Gently Circling Leaves to softlock when two defense cards are drawn.
* Updated the Japanese/Korean localization to match the new version of Gently Circling Leaves
* Fixed minor errors in the localization for Maple Leaf Festival and Instant of Contact
* Added flavor text to most cards.

## 1.1.2

Hotfix Localization

* Updated the localization for Gently Circling Leaves to match its new effect

## 1.1.1

Localization fixes

* Updated Momiji's Korean localization to match current english status effect/card text.
* Added a new Japanese localization for Momiji, created by neff on the Lost Branch of Legend Modding Discord.
* Fixed the code for Rabies Bite to correctly reference Vulnerable instead of Weak
* Fixed the localization of Momiji's exhibits to correct errors related to her last name's spelling.
* Added an effect for when Reflection granted by most of the cards and/or status effects Momiji has access to activates.
* Reworked Gently Circling Leaves. Now grants 3 Reflection per Atttack card drawn, and 4 Block per Defense card drawn.

## 1.1.0

Update for Koishi

* Fixed Momiji to work with Koishi. Will no longer work with earlier versions of LBoL.
* Surge of Power and Indiscriminate Revenge are now Rare.
* Indiscriminate Revenge+ now costs WWRR, instead of 1WR.
* Taunt now deals 4 damage to the player. Taunt+ now deals 2 damage to the player.
* The cost for Stand Watch has been reverted to WWRR, or 1WR upgraded.
* Retaliation now decreases by 1 each turn. Further potential nerfs may come in the future.

## 1.0.12

More Localization Fixes

* Fixed a localization error where Waterfall Meditation said it would aplly Frail instead of Vulnerable.
* Slightly adjusted the balance of Waterfall Meditation to slightly nerf it.
* Fixed a localization error on Black Wind

## 1.0.11

Localization fixes

* fixed localization errors on Black Wind and Taunt
* fixed a bug where Raging Waterfall would cause a softlock on play

## 1.0.10

Miscelaneous bugfixing

* Feed on the Weak is now Rare instead of Uncommon
* Iron Stance now gives Barrier on upgrade
* Fixed a bug where Smell of Death would softlock on play.
* Fixed a bug where Frigid Sky would not correctly work

## 1.0.9

Intention Fixing

* All cards that interact with enemy intention categories(Hunting Call, Parting Slice, Training Record, Always Watching, Double Down, Seize the Moment, Waterfall Meditation) now display what specific intentions are grouped under which category.
* Seize the Moment fixed to properly apply Negative Firepower.
* Overhand Blade, Defensive Strike, and Windswept Blade have all been changed to 2-cost cards. Damage numbers have been tweaked to account for this.
* Stand Watch now costs 1WR unupgraded, or WR upgraded.
* Cleaving Swipe and Rip Apart both have had their damage buffed.
* Twin Fang's Localization has been updated to reflect that it gives the player a small amount of Reflection when played.
* A bug causing Exploit Openings to softlock the game when played targeting an enemy with Amulet or a Drone has been patched.
* New card added to Momiji's card pool: Black Wind, a Black card that makes Air Cutters apply Vulnerable on hit.
* New card added to Momiji's card pool: Taunt, a common Red card that applies weak and vulnerable to an enemy for 1 mana, at the cost fo the enemy attacking Momiji for a small amount of damage.

## 1.0.8

Minor text changes+Balancing

* Fixed the wording on Woodland Sentry to be in line with other cards that give the player life.
* Fixed Waterfall meditation to properly apply Spirit Down.
* Call to Arms now can grab cards from the Discard pile as well as the draw pile
* Rewrote the status effect and card localization for howling Mountain Wind to make it clear that the effect does not stack.
* Added the Basic keyword to Guard Up
* did some internal rewording to make certain that all cards have correct art.

## 1.0.7

More Miscellaneous fixes

* Vacuum Cutter is now Red
* Tireless Guard now grants the player 6 Reflection, or 12 on Upgrade.
* The description for Retaliation now reflects that it gives Reflection when gained.
* Raging Waterfall now grants a generic "cards next turn" status effect, instead of Fairy Intellect

## 1.0.6

Miscellaneous fixes

* Fixed a bug allowing Mountainside Trail Tracking to activate multiple times per turn
* Attempted a fix for a bug letting Distract apply Weak multiple times per card played.
* Fixed a bug where Destabilize would provide Barrier instead of Block.
* Properly formatted Retaliation to gain reflect when gaining new levels of Retaliation, not just when Retaliation is first gained.
* Fixed a bug where Sense Weakness would not stack.


## 1.0.5

Fixing the model.

* Changed the coordinates of Momiji's in-game model so she would actually show up.
* Changed Indiscriminnate Revenge from a Skill to an Ability, and bumped its rarity.
* Changed MomijiB's starting deck to include Eye for an Eye.


## 1.0.4

Korean Translation + a few fixes.

* Indiscriminate Revenge now has actual text.
* Attempted a fix for the two icon problem. Remains to be seen if it breaks something else
* Hunting Hurricanes fixed to deal damage one additional time per exiled air cutter, rather than ten.
* Hunting Hurricanes Localization updated to refleect its AoE nature
* Attempted a fix for Momiji's in-game sprite floating higher than other sprites
* Circular Pacing and Mountaiside Trail Tracking will no longer make attack cards display an error in their text boxes if they are played while the player has either status effect
* attempted a fix to Momiji's Avatar to remove the weird cut-off bit

## 1.0.3

More Focused Balance Changes.

* Retaliation now applies Reflection equal to its level when gained. This is to allow Momiji's white cards to scale faster into the later game.
* Warding Strike now has Accurate.
* Fixed a bug with Always Watching that softlocks the game when it is played
* Added a new card: Indiscriminate Revenge.
* Fixed a bug with Howling Mountain Wind and changed the description of the Status Effect to match the card.
* Fixed a bug with Smell of Death that created a softlock when the card was played.
* Attempted a fix for a bug where Feed on the Weak would not trigger
* Crisp Fall Air's status effect icon will now be visible
* Fixed a bug where Tireless Guard would remove itself after triggering once.
* Changed Riposte from an Ability into a Skill. Further balance changes will come after testing

## 1.0.2

Initial balance changes

* Hunter's Trap+ Mana cost changed to 1R from 2
* Hunting Call fixed to trigger when enemies have an **Offensive Intention**, rather than any other intention.
* Double Down fixed to properly trigger.
* Seize The Moment changed to apply 3/4 Spirit Down if the enemy has a **Defensive Intention**, rather than 2/3 Frail
* Waterfall Meditation changed to apply 1/2 Spirit Down if the enemy has a **Defensive Intention**, rather than 1/2 Frail.

## 1.0.1

* Fixed the mod icon.

## 1.0.0 

Initial Release