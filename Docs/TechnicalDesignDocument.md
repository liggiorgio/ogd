# Technical Design Document

> FRONT PAGE

##	1. Project Goal
Our game aims to provide an online multiplayer experience on Android devices (as described in the **Client Side** section of this document). From a technical standpoint, the game aims to offer 24\7 worldwide availability for all the online features, such as searching for and joining online matches, customizing player profiles, accessing leaderboards and collectible lists.

##	3. Provided Services (besides the game)
In addition to the game itself, we provide a complete Google Play integration to ease access to leaderboards and achievements.

##	4. Client Side

	###	4.1	Hardware requirements
	The minimum hardware requirements are low-to-mid-range smartphones, with the following specifications:
		* At least 50MB of free internal storage
		* A 3G or Wi-Fi Internet connection
		* At least 1GB of RAM

	###	4.2	Software requirements
	The game requires at least Android 5.0 (API level 21). More details on the decision can be found in the **Platform** and **System Requirements** section of the GDD.

##	5. Workload estimation
Target workload for your infrastructure in term of total users, peak users, and resources dedicated to each user.
Starting from an initial system capacity and extend later is fine but then you must provide an extension plan.
MOTIVATE THIS referencing the GDD

##	6. Frontend

	###	6.1	Platforms
	Motivate your selection

	###	6.2	Hardware
	Not just “what” but also “how many”

	###	6.3	Software

	###	6.4	Scalability and Extensibility
	How are you planning to quickly extend you infrastructure when and if needed

##	7.	Backend
Remember to put databases here!

	###	7.1	Platforms
Motivate your selection

	###	7.2 Hardware
Not just “what” but also “how many”

	###	7.3	Software

	###	7.4	Workload capacity
Give some rationale why this infrastructure should stand the intended workload


##	8.	Development

	###	8.1	Hardware
Not just “what” but also “how many”

	###	8.2	Software
Not just “what” but also “how many”

	###	8.3	Major Software Development Tasks
We need a list here

	###	8.4	Development Gantt
![Gantt planning until open beta phase](pictures/gantt.png)

##	9.	External Services
Whatever service you will buy/rent from third parties
If you opt in for cloud, this is NOT the right place, put it in § 6 or § 7

##	10.	Communication	

	###	10.1	Global Infrastructure Outline
How servers are connected (hint: use a picture)
What is installed on each server

	###	10.2	Network Requirements
Bandwidth, latency, type of connection, QoS in general
Inside and outside your infrastructure

	###	10.3	Network Hardware
Not just “what” but also “how many” … if any

##	11.	Delivery

	###	11.1	Estimated Delivery Time
To be compliant with your GANTT

	###	11.2	Delivery Platform
	The game will be delivered through the Google Play store.

	###	11.3	Delivery Methodology
This is about how you are going to use your distribution channel

##	12.	Staff

	###	12.1	For Infrastructure Setup
		Cloud services developer

	###	12.2	For Infrastructure Management
		System Administrator

	###	12.3	In Game
		Senior Game Designer
		Junior Game Designer
		Game Developer
		Graphic Artist

	###	12.4	Other