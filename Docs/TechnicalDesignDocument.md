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
* At least 100 MB of free internal storage
* A 3G or Wi-Fi Internet connection
* At least 512 MB of RAM

###	3.2	Software requirements
The game requires at least Android 4.4 (API level 19). More details on the decision can be found in the **Platform** and **System Requirements** section of the GDD.

##	4. Workload estimation

The rollout plan consists of an initial closed beta, followed by an open beta phase, to test and improve the game based on user feedback, and a release phase, when the game will be fully available to anyone through the Google Play Store. The game in the closed beta phase will use a P2P system for the matches. From the open beta onwards, the online part of the game will be composed of a classic client-server system, with the server hosted on the Google Firebase platform.

The closed beta phase will be rolled out by giving codes for the game via our social network pages. The codes will be redeemable on the Google Play Store and will give exclusive access to the game, thanks to the Play Store Beta Test program.

- Expected average number of daily players: 500
- Expected average number of simultaneous players: 200
- Expected number of total players: 1,500

The open beta phase will be rolled out through the Play Store Beta Test program, same as the closed beta, to make our game readily available on the most trusted source for Android apps.

* Expected average number of daily players: 1,750
* Expected average number of simultaneous players: 700
* Expected number of total players: 5,250

On release, we expect an increase in the number of players
* Expected average number of daily players: 3,750
* Expected average number of simultaneous players: 1,500
* Expected number of total players: 11,250

[comment]: # "TODO: add post-release plan"

The game uses an authoritative client-server system for moves validation. The game is essentially played on the server, with the users sending their moves and receiving the new state of the board from the server. Since everything is hosted on the Firebase platform, resources are allocated and paid as needed, without the need for a specific extension and scalability plan.

### 4.1	Player costs

The following is a rough estimation of the player costs during the open beta period. This is the first period of the game being available to the world and also the moment when we begin using the Firebase services. Since some Firebase services are billed per-access (every time a user opens the game or a specific page) or per-operation (every time data is read or written from\to the database), the costs are estimated taking into account that each daily player will play 10 matches, opening the app twice, simulating an user going from home to work and then returning home. 

| Category                    | Resources per unit       | Total units                      | Total resource usage             | Total cost      |
| --------------------------- | ------------------------ | -------------------------------- | -------------------------------- | --------------- |
| Cloud Functions Invocations | 60 invocations per match | 8,750 matches per day            | 15,750,000 invocations per month | $ 5.60 (€ 5)    |
| Outbound traffic            | 1 KB per invocation      | 15,750,000 invocations per month | 15.75 GB per month               | $ 1.20 (€ 1.10) |
| GB-seconds                  | 100ms per invocation     | 15,750,000 invocations per month | 1,575,000 GB-s per month         | $ 3 (€ 2.7)     |
| CPU-seconds                 | 100ms per invocation     | 15,750,000 invocations per month | 1,575,000 CPU-s per month        | $ 14 (€ 12.6)   |
|                             |                          | **Total**                        | € 21.4 per month                 | **€ 64.2**      |

[comment]: # "Target workload for your infrastructure in term of total users, peak users, and resources dedicated to each user. Starting from an initial system capacity and extend later is fine but then you must provide an extension plan. MOTIVATE THIS referencing the GDD"

##	5. Frontend

The frontend receives all the players' interaction and sends them to the correct backend service to be handled. Since some of our services are provided through Google platforms like Google Play Games (from now on called GPG) and Google Play Store (for payments), our frontend services will collect user requests and forward them to the correct external services, handling the returned data.

The Authentication service will handle the user request and receive the user ID from the GPG authentication API. This ID will then be used to send matchmaking requests (to the GPG matchmaking service) and to save user data.

The Payments service will handle and forward the users' in-app purchases to the Google Play payments API. It will then send the transaction's result to the User data backend service to give the user the correct amount of coins.

###	5.1	Platforms
Our solution of choice is a Platform-as-a-Service set of products, Firebase, provided by Google. Firebase lets us deploy our frontend services to their Functions product, that will be used for both the frontend and the backend.

###	5.2	Scalability and Extensibility
Since our solution of choice is the Firebase cloud platform, the system automatically scales according to the needs of our userbase. In case of an increase in the number of players, Firebase automatically adjusts the provided compute time to suit our needs.

##	6.	Backend
Our game uses two main backend services to handle the player data and the match themselves. 
Our services are:

* Game Service: this service receives player moves, validates them, makes changes to the game board according to the information received and sends the result back to the client. It is the core of the online game experience.
* User Data Service: this service is used to read or update user data, like their friend list, the amount of coins they own and other information. Since we use the GPG Saved games capability to handle the player data, nothing is stored on our databases. Everything is received from GPG, updated as needed, and then uploaded to GPG. Since this service uses the players' own Google Drive storage space to save data, and since traffic between Google services is free, it comes at no cost to us.

###	6.1	Platforms and software
As said in the **Frontend** section, we use Firebase as our cloud platform of choice. The decision was made after considering what best suits our needs for an affordable, scalable and easily manageable backend platform. We chose Firebase because it has low initial costs (most of its products are free under a certain data threshold) and fine-grained billing plans, is very well integrated with the whole Google environment (since it is also owned by Google) and provides a specific SDK for game development in Unity.   
Once this choice is done, we have no control over which software Firebase uses but we can access all of their products, many of which have, as previously stated, a monthly usage threshold under which they are provided for free.

To upload our own game services, we will use Firebase's Function product. It is essentially a higher-level version of Google Cloud Functions that aims to provide the same service with a much lower configuration effort and mobile-optimized tools.

###	6.2	Workload capacity

Since our backend is completely cloud-based, we can expect new computational power and storage space to be provided as needed. So, the system should be capable of handling a very large amount of simultaneous matches should the need arise.

##	7.	Development

All the monthly costs are relative to the period between the beginning of the project and the Open Beta release.

###	7.1	Hardware
| Product                     | Quantity | Description                                                  | Cost per Unit | Total Cost |
| --------------------------- | -------- | ------------------------------------------------------------ | ------------- | ---------- |
| Dell Precision 3630 Tower   | 3        | High performance desktop workstation for the whole team. Windows license, mouse and keyboard included. | € 1,350       | € 4,050    |
| Dell UltraHD 24 - P2415Q    | 3        | High resolution and colour accurate screen for the whole team. | € 465         | € 1,395    |
| Synology DS918+             | 2        | 4-bay NAS enclosures, for storage and backup purposes.       | € 580         | € 1,160    |
| Western Digital Red 3TB     | 8        | NAS-specific HDDs for the Synology enclosures.               | € 119         | € 952      |
| Netgear GS305E              | 2        | 5 port web managed ethernet switch.                          | € 34          | € 68       |
| 5-pack Ethernet Cat6 cables | 2        | 3m long Ethernet cables to connect everything to the ISP-provided modem\router | € 14          | € 28       |

###	7.2	Software
| Product | Quantity | Cost | Total Cost |
| -- | -- | -- | -- |
| Unity Pro | 3 | € 115 per month per seat | € 2,070 |
| Visual Studio Professional | 1 | € 1200 first year | € 1,200 |
| Git | 3 | € 0 | € 0 |
| GIMP | 3 | € 0 | € 0 |

[comment]: # "https://www.ibaudio.com"

### 7.3	Other

| Product                       | Unit cost                                          | Total cost |
| ----------------------------- | -------------------------------------------------- | ---------- |
| Office Rent (incl. utilities) | € 1000 per month                                   | € 6,000    |
| ISP - Gigabit connection      | € 20 per month (€ 30 per month after the 1st year) | € 120      |
| Royalty-free music            | $ 69 per year (approx. € 62)                       | € 62       |

[comment]: # "https://www.timbusiness.it/fisso/offerte-fibra-adsl-telefono/senza-limiti-fibra"

### 7.4 	Development costs

| Description | Cost         |
| ----------- | ------------ |
| Software    | € 3,270      |
| Hardware    | € 7,653      |
| Other       | € 6,182      |
| **Total**   | **€ 17,105** |



### 7.5	Main software development tasks

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
- Friends management

#### GUI Development

- Login screen
- Main menu and Options menu
- In-game UI
- In-game shop
- Lobby\Loading screen
- Level selection screen
- Friend list screen

#### Art & Animations

- Faces design and animations
- Card visual design
- Character design
- Game backgrounds
- Card activation animations
- Win\Loss screen
- Campaign dialogues

###	7.6	Development Gantt

![Gantt planning until open beta phase](pictures/gantt.png)

##	8.	External Services
To simplify our network and to avoid potential mismanagement of heavily sensitive data, we delegate the payment service to Google, the publisher of our game. This has no upfront cost (except for the € 25 license needed to publish the game) but Google takes 30% out of every transaction as management fees.

We also decided to outsource some services, specifically customer support, 2D art and marketing, to lower their costs and to use them only when effectively needed.

### 8.1	2D Art

We got in touch with a bunch of freelance artists, asking them for quotes for the amount of work we requested. Most of the quotes were around € 40 per drawing, for 20 drawings. This gives us a total of approx. € 800.

### 8.2	Customer Support

We can't foresee or estimate how many users will need customer support for our product, so we opted for a pay-per-ticket service, that will scale accordingly if for some reason the amount of support needed rises unexpectedly. For budget reasons, we expect 4% of our player base to incur in problems each month. Since we expect around 5,250 total players during the open beta, there will be 210 players in need of support each month. 

This, according to the quote given by the company we plan to outsource to, amounts to $ 599 (€ 536) each month. Estimating a 3 month long open beta, this totals to € 1,608.

### 8.3	Marketing and Social media management

We plan to begin advertising our game on the main social medias (Facebook, Instagram) from two weeks before the beginning of the closed beta, to reach a good number of potential players from before the release of the game. Expense for ads and duration of ad campaigns will be decided with our outsourced social media manager, whose cost according to the market is about € 3,000 each month. As a purely budgetary estimate, ads reaching an audience of about 200,000 users on Facebook and Instagram will cost € 500 a day. Running this campaign for a month sums up at approx. € 15,000.

Considering this a prototype campaign for the open beta phase and summing up 4 months of social media managing gives a gross total of € 27,000 for preliminary marketing and social media management.

### 8.4	Cost estimation

| Service                               | Total cost   |
| ------------------------------------- | ------------ |
| Google Licensing                      | € 25         |
| 2D art                                | € 800        |
| Customer Support                      | € 1,608      |
| Marketing and Social media management | € 27,000     |
| **Total**                             | **€ 29,433** |



##	9.	Communication

###	9.1	Global Infrastructure Outline

![Game network outline](D:\Code\ogd\Docs\pictures\network.png)
[comment]: # "How servers are connected (hint: use a picture What is installed on each server"

###  9.2  Network Requirements

#### Client side:

A 3G connection is required to play the game.

#### Server side: 

Low-latency services with up to 500Mbps connection.

##	10.	Delivery

###  10.1  Estimated Delivery Time

We initially plan to roll out codes for a closed beta of our game, about 6 months after the beginning of development. We then estimate to be ready for an open beta phase after 1 or 2 months of work, as shown in the **Development Gantt** section, and to be ready to release the game after 10 to 12 months since the beginning of development, 3 to 4 months after the open beta phase. This is an optimistic estimate, since the precise release dates for the betas and the final product should be carefully planned with marketing experts and there is always the risk of external events delaying the development process.

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

### 11.2	Outsourced 

Outsourced staff is called whenever needed or "rented" each month without being effectively part of the company.

- Social Media Manager
- 2D\Pixel Artist