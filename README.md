# FG-Application

# Brief explanation of the project:


# Added Fetures

* MainWindow screen that allows you to upload a csv and xml files that necessary for the flight simulator.
* Main screen that contains joystick and sliders of throttle and rudder, that moves according to the movement of the airplane.
* There are measurements like altimeter, airspeed and heading. Also, the user can see a visualization of the pitch, roll and yaw.  
* There is a list of all the flight attributes, the user can select one of them at a time, and then, three graphs that related to the attribute will show up.
* MediaBar that allows the user variety of actions: change the speed of the video, stop, continue, go back to the beginning etc.  


# Required installation and Preparations

1) [FlightGear](https://www.flightgear.org/) application version 3.6 
2 Move playback_small.xm to C:\Program Files\FlightGear 2020.3.6\data\Protocol.
3) Using the .NET Framework to create a GUI App for FlightGear.
4) Visual Studio 2019 
5)install the plotting library for .NET named [OxyPlot](https://oxyplot.readthedocs.io/en/latest/getting-started/hello-wpf-xaml.html)


# Compiling and Running

Download the zip for this repository or use git on the termianl. The terminal command is:

git clone https://github.com/Noamls123/FG-Application

Build the project.


# UML Chart and Design pattern

You can press [here](https://github.com/Noamls123/FG-Application/blob/main/UML.jpeg) to get the UML of the main classes.


# Code Design and Architechture:

For programming this app, **MVVM** architecture.

This App has three main parts that run it, each part with its own designated responsibilities- View, ViewModel and Model.
The **Model**, as a client, interacts with The server (the Flight-Gear app) via TCP connection, and send data of csv and xml files.
In addition, the Model notifies the relevant **ViewModel** when it's data changed. Next, the ViewModel process the changed data and notifies the **Views** (all the UserControls which appear on the screen) by using data binding mechanism of wpf. The Views reacts to the changed data accordingly.
Furthermore, When the data of the View changed by the user, it is send a command to the relevant ViewModel, that send a command to the Model which reacts to the changed data accordingly. 


# Collaborators
This program was developed by Shaked Arel, Ruth Lofsky, Hadassa Danesh and Noan Sery Levi.





