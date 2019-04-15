# Technical Design Document

> FRONT PAGE

##	1. Project Goal
Our game aims to provide an online multiplayer experience on Android devices (as described in the **Client Side** section of this document). From a technical standpoint, the game aims to offer 24\7 worldwide availability for all the online features, such as searching for and joining online matches, customizing player profiles, accessing leaderboards and collectible lists.

## 2. Provided Services (besides the game)
In addition to the game itself, we provide a complete Google Play Games integration to ease access to leaderboards and achievements.

##	3. Client Side

###	3.1	Hardware requirements
The minimum hardware requirements are low-to-mid-range smartphones, with the following specifications:
* At least 50MB of free internal storage
* A 3G or Wi-Fi Internet connection
* At least 1GB of RAM

###	3.2	Software requirements
The game requires at least Android 5.0 (API level 21). More details on the decision can be found in the **Platform** and **System Requirements** section of the GDD.

##	4. Workload estimation
The online part of the game is composed of a classic client-server system, with the server hosted on the Google Firebase platform. 
The rollout plan consists of an open beta phase, to test and improve the game based on user feedback, and a release phase, when the game will be fully available to anyone through the Google Play Store.

The open beta phase will be rolled out through the Google Play Beta Test Management plan, to make our game readily available on the most trusted source for Android apps.
* Expected average number of daily players: 1,000
* Expected average number of simultaneous players: 500
* Expected number of total players: 5,000

On release, we expect an increase in the number of players
* Expected average number of daily players: 15,000
* Expected average number of simultaneous players: 4,500
* Expected number of total players: 25,000 

Each match is played on the server. Each player sends the server their move, that is then verified and executed. The results are sent back to the client and then visualized on the player's screen. Since this must happen quasi-real-time, each user (or better, each match) has enough dedicated resources to be able to compute everything in the required time. Since everything is hosted on the Firebase platform, resources are allocated and paid as needed.

[comment]: # (Target workload for your infrastructure in term of total users, peak users, and resources dedicated to each user. Starting from an initial system capacity and extend later is fine but then you must provide an extension plan. MOTIVATE THIS referencing the GDD)

##	5. Frontend

Players will access frontend services from the game itself, logging in with their Google account through the Google Play Games platform. The authentication service is provided by the Google Play Games services and provides us with information on the player's account ID, display name and profile image. The frontend also receives all the players' interaction and sends them to the correct backend service to be handled.

###	5.1	Platforms

###	5.2	Scalability and Extensibility
Since our solution of choice is the Firebase cloud platform, the system automatically scales according to the needs of our userbase. In case of an increase in the number of players, Firebase automatically adjusts to our needs.

##	6.	Backend
Remember to put databases here!

###	6.1	Platforms
Motivate your selection

###	6.2 Hardware
Not just “what” but also “how many”

###	6.3	Software

###	6.4	Workload capacity
Give some rationale why this infrastructure should stand the intended workload


##	7.	Development

###	7.1	Hardware
Not just “what” but also “how many”

###	7.2	Software
Not just “what” but also “how many”

###	7.3	Major Software Development Tasks
We need a list here

###	7.4	Development Gantt
![Gantt planning until open beta phase](pictures/gantt.png)

##	8.	External Services
Whatever service you will buy/rent from third parties
If you opt in for cloud, this is NOT the right place, put it in § 6 or § 7

##	8.	Communication	

###	8.1	Global Infrastructure Outline
How servers are connected (hint: use a picture)
What is installed on each server

###	8.2	Network Requirements
Bandwidth, latency, type of connection, QoS in general
Inside and outside your infrastructure

###	8.3	Network Hardware
Not just “what” but also “how many” … if any

##	9.	Delivery

###	9.1	Estimated Delivery Time
To be compliant with your GANTT

###	9.2	Delivery Platform
The game will be delivered through the Google Play store.

###	9.3	Delivery Methodology
This is about how you are going to use your distribution channel

##	10.	Staff

###	10.1	For Infrastructure Setup
Cloud services developer

###	10.2	For Infrastructure Management
System Administrator

###	10.3	In Game
Senior Game Designer
Junior Game Designer
Game Developer
Graphic Artist

###	10.4	Other