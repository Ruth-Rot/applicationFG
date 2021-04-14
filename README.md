# applicationFG

# Brief explanation of the project:

This App has three main parts that run it, each part with its own designated responsibilities.
The Model, as a client, interacts with The server (the Flight-Gear app) via TCP connection, and send data of csv and xml files line by line.
In addition, the Model notifies the relevant ViewModel when it's data changed. Next, the ViewModel process the changed data and notifies the Views (all the UserControls which appear on the screen). The Views reacts to the changed data accordingly.
Furthermore, When the data of the Views changed by the user, it send a command to the relevant ViewModel, that send a command to the Model which reacts to the changed data accordingly. 

# Added Fetures

* MainWindow screen that allows you to upload a csv and xml files that necessary for the flight simulator.
* Main screen that contains joystick and sliders of throttle and rudder, that moves according to the movement of the airplane.
* There are measurements like altimeter, airspeed and heading. Also, the user can see a visualization of the pitch, roll and yaw.  
* There is a list of all the flight attributes, the user can select one of them at a time, and then, three graphs that connect to the attribute will show up.
* MediaBar that allows the user varies of actions: change the speed of the video, stop, continue, go back to the beginning etc.  










1) Using the .NET Framework to create a GUI App for FlightGear.

Third main achievements:

1)


and special features that we have implemented:
