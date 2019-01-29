# NetworkPlugin


### Description

This project provides the opportunity to convert a VR-singleuserapplication to a VR-multiuserapplication with minimal effort. It also includes a VR-Lobby and the possibility to select avatars.

### Installation

+ download Project
+ insert the Gizmo and UnityNetworking-Folders into the Assetfolder of your Project (they include Photon, VRTK, SteamVR and PhotonVoice)
+ if you have already installed Photon, VRTK, SteamVR and PhotonVoice you can just copy the NetworkPlugin-Folder into your Project (could lead to versioning problems)
+ to setup you need a [Photon](https://dashboard.photonengine.com/en-US)-Account (free) 
+ use the Photon-Dashboard to generate an AppId and a VoiceChatId 
+ set the Photon-Server-Settings in your Project

### Instruction

After installing the menu "Network" will appear at the menubar. This menu contains the following menuitems
+ Setup Basic Network
+ Spawn Positions
+ Object Sync
+ Help

Open the scene you want to have multiplayersupport and submit the Setup Basic Network. This adds the gameobject "CONNECT" to the scene. It contains the components PhotonView, PlayerManger, ShowStatusWhenConnecting and PhotonVoiceSettings. They come with default configurations and can be further configured. Mostly you want to configure the PlayerManager with the following options:
+ PlayerAvatar (Drag and Drop a Prefab of an Avatar)
+ JoinRandomRoom (must be set true if you don't join over the lobby)
+ MayPlayersPerRoom (sets the maximum players)

Spawn Positions opens a new gui where you can add spawn positions. They will be added as gameobjects to your scene and can also be moved inside your scene. Remember that you need at least one spawn position.

Object Sync also opens a new gui where you can choose between synchronizing gameobject/s and interactable-gameobject/s. Just select the object/s you want to synchronize in the hierarchy and push the corresponding button. In the scene-window your synchronized object/s will now be highlighted with a gizmo. 

The helpmenu links to this repository.


### Lobby
There are two lobbyscenes, both of them contain the Launchers-gameobject. It contains the Launchers.cs-script with the following configurable options:
+ LogLevel, default full.
+ MaxPlayersPerRoom, default 7 (sets the maximum players). The free Photon-version allows a maximum of 20 players per room.
+ LoadSceneNext, drag & drop the scene you want to load out of your project-folder.
+ Avatar, default 2 with the PlayerAvatar and the HorseAvatar. To add addtional avatars you have to set the size of the list and drag & drop the avatars you want out of your project-folder. 
