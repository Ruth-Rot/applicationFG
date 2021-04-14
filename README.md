# applicationFG

# Brief explanation of the project:

This App has three main parts that run it, each part with its own designated responsibilities.
The Model, as a client, interacts with The server (the Flight-Gear app) via TCP connection, and send data of csv and xml files line by line.
In addition, the Model notifies the relevant ViewModel when it's data changed. Next, the ViewModel process the changed data and notifies the Views (all the UserControls which appear on the screen). The Views reacts to the changed data accordingly.
Furthermore, When the data of the Views changed by the user, it send a command to the relevant ViewModel, that send a command to the Model which reacts to the changed data accordingly. 



The Model interacts with The server (the Flight-Gear app or server script) via TCP connection, continuously send and read data requests and notifies the relevant ViewModel when it's data changed. Next, the ViewModels process the changed data and notifies the Views about the changed data. the Views reacts to the changed data accordingly, the data flows both from and to the Model.






1) Using the .NET Framework to create a GUI App for FlightGear.

Third main achievements:

1)


and special features that we have implemented:
