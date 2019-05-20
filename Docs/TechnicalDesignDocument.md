# Technical Design Document

![Alpha Commit logo](./pictures/ac_logo_small.png)  
`A L P H A // C O M M I T`

### General information
**Team name:** “Alpha Commit”  
**Team members:** Baratto Diego (919538), Costella Alessandro (920031), Liggio Giorgio Maria (905471)

---

##	1. Project Goal
Our game aims to provide an online multiplayer experience on Android devices. From a technical standpoint, the game aims to offer 24\7 worldwide availability for all the online features, such as searching for and joining online matches, customizing player profiles, accessing leaderboards and collectible lists.

## 2. Provided Services (besides the game)
In addition to the game, we aim to provide the following services to the player:

- Access to leaderboards and achievement lists (through Google Play Games);
- Customer support.

##	3. Client Side

###	3.1	Hardware requirements
The minimum hardware requirements are low-to-mid-range smartphones, with the following specifications:
* At least 50MB of free internal storage
* A 3G or Wi-Fi Internet connection
* At least 1GB of RAM

###	3.2	Software requirements
The game requires at least Android 5.0 (API level 21). More details on the decision can be found in the **Platform** and **System Requirements** section of the GDD.

##	4. Workload estimation

TODO EXTEND TODO EXTEND TODO EXTEND

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

[comment]: # "Target workload for your infrastructure in term of total users, peak users, and resources dedicated to each user. Starting from an initial system capacity and extend later is fine but then you must provide an extension plan. MOTIVATE THIS referencing the GDD"

##	5. Frontend

The frontend receives all the players' interaction and sends them to the correct backend service to be handled. Players will access frontend services from the game itself, logging in with their Google account through the Google Play Games platform. The authentication service is provided by the Google Play Games services and provides us with information on the player's account ID, display name and profile image.

###	5.1	Platforms
Our solution of choice is a Platform-as-a-Service set of products, Firebase, provided by Google. Firebase lets us deploy our frontend services to their Functions product, that will be used for both the frontend and the backend.

###	5.2	Scalability and Extensibility
Since our solution of choice is the Firebase cloud platform, the system automatically scales according to the needs of our userbase. In case of an increase in the number of players, Firebase automatically adjusts the provided compute time to suit our needs.

##	6.	Backend
Our game uses three main backend services and two external components. The external components are used to handle user authentication and microtransaction and are both provided by Google, that lets us have information about the player and confirmation of successful transactions.  
Our services are:

* Game Service: this service receives player moves, validates them, makes changes to the game board according to the information received and sends the result back to the client. It is the core of the online game experience.
* User Database: a database containing all the information regarding our players, like their achievements, their win\loss ratio and their collected cards. It also has a secondary database used as backup.

###	6.1	Platforms
As said in the **Frontend** section, we use Firebase as our cloud platform of choice. The decision was made after considering what best suits our needs for an affordable, scalable and easily manageable backend platform. We chose Firebase because it has low initial costs (most of its products are free under a certain data threshold) and fine-grained billing plans, is very well integrated with the whole Google environment (since it is also owned by Google) and provides a specific SDK for game development in Unity.   
Once this choice is done, we have no control over which software Firebase uses but we can access all of their products, many of which have, as previously stated, a monthly usage threshold under which they are provided for free.

###	6.2	Software

| Service | Solution |   Reason   |
| -- | -- | -- |
| Game Service | Cloud Functions  | This service lets us host our game matches on Firebase's servers, reacting to actions like player moves or match timeouts.  |
| User Database | Cloud Firestore | This service provides a robust cloud-hosted NoSQL database with efficient queries and offline availability, letting us store all player data while also making them accessible in a read-only fashion if the user is offline. |

###	6.3	Workload capacity
Since our backend is completely cloud-based, we can expect new computational power and storage space to be provided as needed. So, the system should be capable of handling a very large amount of simultaneous matches should the need arise.

##	7.	Development

All the monthly costs are relative to the period between the beginning of the project and the Open Beta release.

###	7.1	Hardware
| Product                     | Quantity | Description                                                  | Cost per Unit | Total Cost |
| --------------------------- | -------- | ------------------------------------------------------------ | ------------- | ---------- |
| Dell Precision 3630 Tower   | 3        | High performance desktop workstation for the whole team. Windows license, mouse and keyboard included. | € 1.350       | € 4.050    |
| Dell UltraHD 24 - P2415Q    | 3        | High resolution and colour accurate screen for the whole team. | € 465         | € 1.395    |
| Synology DS918+             | 2        | 4-bay NAS enclosures, for storage and backup purposes.       | € 580         | € 1.160    |
| Western Digital Red 3TB     | 8        | NAS-specific HDDs for the Synology enclosures.               | € 119         | € 952      |
| Netgear GS305E              | 2        | 5 port web managed ethernet switch.                          | € 34          | € 68       |
| 5-pack Ethernet Cat6 cables | 2        | 3m long Ethernet cables to connect everything to the ISP-provided modem\router | € 14          | € 28       |

###	7.2	Software
| Product | Quantity | Cost | Total Cost |
| -- | -- | -- | -- |
| Unity Pro | 3 | € 115 per month per seat | € 2.070 |
| Visual Studio Professional | 1 | € 1200 first year | € 1.200 |
| Git |  | € 0 | € 0 |

[comment]: # "https://www.ibaudio.com"

### 7.3	Other

| Product                       | Unit cost                                          | Total cost |
| ----------------------------- | -------------------------------------------------- | ---------- |
| Office Rent (incl. utilities) | € 1000 per month                                   | € 6.000    |
| ISP                           | € 20 per month (€ 30 per month after the 1st year) | € 120      |
| Royalty-free music            | $ 69 per year (approx. € 62)                       | € 62       |
|                               |                                                    |            |

[comment]: # "https://www.timbusiness.it/fisso/offerte-fibra-adsl-telefono/senza-limiti-fibra"

### 7.4 	Development costs

| Description | Cost         |
| ----------- | ------------ |
| Software    | € 3.270      |
| Hardware    | € 7.653      |
| Other       | € 6.182      |
| **Total**   | **€ 17.105** |



### 7.4	Main software development tasks

#### Programming

**Server-side**

- User database management
- Game logic
- Move validation system
- AI
- Game currency management
- Google services interfaces
- Leaderboard management

**Client-side**

- Game logic
- Network communication interface
- Sound and GUI integration
- Social networks integration

#### GUI Development

- Login screen
- Main menu and Options menu
- Player profile screen
- Card Collection screen
- In-game UI
- In-game shop
- Lobby\Loading screen

#### Art & Animations

- HAPPY FACE THINGIES BETTER NAME PENDING
- Card visual design
- Character design
- Game backgrounds
- Card activation animations
- Win\Loss screen
- Player profile screen
- Campaign dialogues

###	7.5	Development Gantt

![Gantt planning until open beta phase](pictures/gantt2.png)

##	8.	External Services
To simplify our network and to avoid potential mismanagement of heavily sensitive data, we delegate the payment service to Google, the publisher of our game. This has no upfront cost (except for the € 25 license needed to publish the game) but Google takes 30% out of every transaction as management fees.

We also decided to outsource some services, specifically customer support, 2D art and marketing, to lower their costs and to use them only when effectively needed.

-PART ABOUT CUSTOMER SUPPORT

-PART ABOUT PRELIMINARY MARKETING

### 8.4	Cost estimation

| Service          | Total cost |
| ---------------- | ---------- |
| Google Services  | € 25       |
| 2D art           |            |
| Customer Support |            |
| Marketing        |            |



##	9.	Communication

###	9.1	Global Infrastructure Outline

TODO TODO TODO TODO TODO TODO MAKE PIC

At this early stage of development, architecture design establishes a centralized server to which client applications connect to on app start. Since main services (account management, database storage...) are provided by the Firebase framework, this will be the main component installed on the proprietary server. Other third-party services (such as Google Play Games) are not the development team's concern.
[comment]: # "How servers are connected (hint: use a picture What is installed on each server"

###  9.2  Network Requirements

TOREDO TOREDO TOREDO

As a matter of early estimate, server-side network must satisfy minimal requirements for testing-purpose only during the development & testing phases. No more than a dozen devices are to connect simultaneously during prototypization, and a common 30 Mbps broadband connection will be enough for this purpose. As soon as the game enters beta testing phase, these requirements may change.

##	10.	Delivery

###  10.1  Estimated Delivery Time

We estimate to be ready for an open beta phase after 6 months of work, as shown in the **Development Gantt** section, and to be ready to release the game after 9 to 10 months, 3 to 4 months after the beginning of the beta phase. This is an optimistic estimate, since the precise release dates for the beta and the final product should be carefully planned with marketing experts and there is always the risk of external events delaying the development process.

###	10.2	Delivery Platform
The game will be delivered through the Google Play Store.

###	10.3	Delivery Methodology
The game will be initially delivered through the Beta Tester program of Google Play Store, in an incomplete but fully playable state. In the following months we will deliver patches and new content on the same platform, until the full release. We will then opt out of the Beta Tester program and continue to release updates through the same platform.

##	11.	Staff

###	11.1	For Game Development
The in-house permanent staff is composed by a team of 3: 

- Game Director and Lead Designer
- Game and Level Designer
- Game Programmer

Since most of the non-permanent staff will be outsourced VI PREGO AIUTO NON SO COSA SCRIVERE