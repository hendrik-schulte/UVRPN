UVRPN by Hendrik Schulte

Latest releases: https://github.com/hendrik-schulte/UVRPN

Getting Started

1.  Add a VRPN_Manager component to any object in the scene.
        Hostname: The IP address or hostname of the VRPN server / localhost.

2.  Add a VRPN_Tracker, VRPN_Button or VRPN_Analog component to any object you want to track with VRPN. 
	Configure it as follows:
        Host reference: The GameObject with the VRPN_Manager component you want to use for this object.
        Tracker: The name of the tracker on the VRPN server.
        Channel: Channel on the server.

