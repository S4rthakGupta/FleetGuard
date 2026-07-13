## What I did

I opened `DevicesController.cs` and added breakpoints inside the `RegisterDevice()` method.

I placed breakpoints around:

- Serial-number normalization
- Duplicate serial-number checking
- The decision point before returning a conflict response

Then I started the ASP.NET Core application using **Run and Debug** in VS Code.
<img width="1287" height="1042" alt="image" src="https://github.com/user-attachments/assets/145e210f-71e3-409c-8f28-79745b343df3" />

After the backend started, I submitted a new device registration request from the frontend.

- Filled the details to register a device.
<img width="1508" height="940" alt="image" src="https://github.com/user-attachments/assets/a3538fc9-b6ae-4513-8ff7-418a5d2774cc" />

- After that it kept showing Registering in UI...
<img width="1507" height="945" alt="image" src="https://github.com/user-attachments/assets/62565cc3-9f56-4f50-af9e-aedd5329d38f" />

- VS Code shows the yellow highlight line. The code execution was stopped here. At this point, the application is paused and I can inspect all local variables.
<img width="1292" height="1053" alt="image" src="https://github.com/user-attachments/assets/6ddb6ed6-a8fd-4360-b87a-a065eb635938" />


## Values I inspected

In the Variables panel, I can see:
- serialNumberExists = false
- device = null
- normalizedSerialNumber = "DT-001"

This tells me:
- The serial number was successfully normalized
- Entity Framework checked the database
- No existing device uses this serial number
- The new Device object has not been created yet because execution has not reached that part of the method

<img width="638" height="300" alt="image" src="https://github.com/user-attachments/assets/99f1be6c-12cc-4be3-a0c3-93c2474603b9" />


- Since the serial number as unique and it passed the validation. It jumped the if statement and reached the new device registration block and can now see more details about the device like Battery, HealthMessage, etc.
<img width="769" height="844" alt="image" src="https://github.com/user-attachments/assets/4f02f68c-5706-4184-b2c0-66a3d4404edf" />

- The controller then returned 201 Created with the newly created Device object.
<img width="926" height="79" alt="image" src="https://github.com/user-attachments/assets/2a1ed341-f1b0-4be0-89d5-25cef76d7b57" />
