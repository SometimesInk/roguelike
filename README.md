# Game Design

This is a roguelike, thus is must follow these roguelike principals:

- Perma-death;

Death is game over, nothing kept between runs except
unlockables. [Items kept between runs seem to have caused controversy in previous games](https://www.reddit.com/r/roguelikes/comments/5e7lzf/comment/daau480/?utm_source=share&utm_medium=web3x&utm_name=web3xcss&utm_term=1&utm_content=share_button).

- Always leave room for skill;

Even when having abysmal luck, a skilled user should still be able to succeed. This includes items that kill on pickup
leaving no room for skill. This game must have randomness, but it shall be controlled to an extent such that the game is
never completely against the user.

- Grindless;

Games should be somewhat short, (up to an hour, perhaps[^1]) thus reducing grinding for resources.

- Enemies grow faster than the player;

Balancing should be calibrated not on a normal user, but slightly harder such that users have no choice but to get
creative to find solutions and tactics.

# **Enemy Design**

Enemies shall be simple to understand, yet difficult to master. These should all follow the following rules:

- Balancing strengths and weaknesses;

Enemies should have at least a weakness. Out of the following three categories, an encounter should have at most two:
health (which includes defense and potential resistances), damage (the damage dealt by the enemy), number (the amount of
enemies). An encounter which has all three should be avoidable.

- Reward difficulty;

An enemy should reward the winning player with a proportional amount of goods in whatever form it may be, as long as the
reward holds its value. For a reward to *hold its value*, it must be usable by the player, or at least not be useless.

## **Combat Philosophy**

As the game progresses, the quantity of fights should be lowered, and their difficulty increased.

# Game Itself

Now we shall board said game: the execution of said principals and rules.

# **Story**

Having a story that is understandable and has a meaning to the player is important; here it is.

## **Main Premise**

The player is in an underground village who knows but rumors about the surface. This adventurous player goes on a quest
to reach the surface fueled by curiosity, and hope. Each *floor* – which is quantized by an amount of light; at the
beginning, the light from the village still reaches the player, then it dims – has the player finding ruins of cities.
The light then hits a point of total darkness, then slowly rises back as the light from the surface grows stronger. This
beam from above is not pure, though: enemies become stronger with this illumination. When reaching the surface, the
player discovers a world of corruption and sadness.

## **World Background**

The world used to live at the surface long ago. People of all different origins lived in harmony and used magic for
their common benefit: better agricultural outputs, distribution of riches… One day, a powerful wizard sought chaos and,
from his tower, caused light to become a poison to those hopeful, and a source of decadence for others. Terrified,
civilians escaped underground and formed cities throughout the caves – most to no avail. They’re life depended on magic,
which itself depended on starlight: hidden below the surface, they couldn’t use their arcane knowledge. Some developed a
different type of magic which does not require the night sky to function: only these prevailed. Hidden underground, one
of these surviving groups forgot its history – the one who the player comes from. Above the earth, the toxic effects of
light slowly vanished, only its power to turn those hopeless into monsters remained.

## **Potential Alternative Endings**

The player could perhaps go further down instead of up and discover another village. Or the player could go up, find the
wizard, and end the corruption.

# **Floors**

Here is a list of floors, their theme, and contents.

| Floor   | Light                       | Theme                            | Contents                                                                  | End?             |
|:--------|:----------------------------|:---------------------------------|:--------------------------------------------------------------------------|:-----------------|
| **1**   | Village light               | The village and its surroundings | Basic underground enemies for the player to understand the combat system. | Boss enemy       |
| **2**   | Dim village light           | Cave                             | “                                                                         | Boss enemy       |
| **3**   | Dark                        | “                                | “                                                                         | First ruins      |
| **4-5** | Total darkness              | Cave with glowing lichen         | Glowing monsters                                                          | Boss enemy/Ruins |
| **6**   | Dark                        | Cave                             | Monsters                                                                  | Boss enemy/Ruins |
| **7**   | Slight surface illumination | “                                | “                                                                         | “                |
| **8**   | Increased surface light     | “                                | “                                                                         | “                |
| **9**   | Illuminated from above      | “                                | “                                                                         | “                |
| **10**  | “                           | “                                | Exit to the surface                                                       | Boss enemy?      |

# **Combat**

The combat system shall be turn based and built considering multiplayer interactions. During combat, the player can use
any item they have in their inventory that can be used during combat. Fights are turn-by-turn where each entity can
attack one at a time using defined abilities. Abilities always succeed to hit. Enemies choose randomly on a weighted
attack pool.

[^1]:  The length of a run shall be calibrated to be somewhat similar to Balatro runs.
