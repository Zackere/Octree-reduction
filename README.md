# Octree image reduction

This project is aimed at implementing an Octree data structure used in image color palette reduction.
## Getting Started

Clone this repo in order to get a copy of all source files necessary. If you have Microsoft Visual Studio installed on your machine, open Octree-reduction.sln file and press F5 to run the program. Remember, that Octree and Octree-gui must be built both in 32 or 64 bit. To change Octree-gui platform target right-click on Octree-gui project, then in Build change Platform target to desired platform. 

## User Interface
![UI](/UI.PNG)
User interface is split in 4 parts:
1. In upper-left corner raw image is displayed.
2. In lower-left corner there are:
	* Slider which determines how many colors final pallette should have
	* A button which starts the reduction process
	* A button which uploads the image to the program
	* Optimization level textbox: 0-none (might be very slow sometimes), 3-max optimization. Setting this value will take an effect upon starting next reduction process.
3. In upper-right corner there is an image which is a result of the reduction after all colors have been inserted
4. In lower-right corner there is an image which is a result of the reduction along insertion

All buttons are disabled until all work is done.
