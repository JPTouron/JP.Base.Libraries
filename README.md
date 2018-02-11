# JP.Base.Libraries

Basic libraries to assist on n-layer design an programming projects and also on mvp styled projects and winforms.

## What this project is not
This is not a framework! this project is supposed to ease-in the design and build of any other common project where you have to deal with: 
>  - presentation of data
>  - interacting with databases (EF6 in this case)
>  - separating your different solution's responsibilities into layers as much as possible

By inheriting from the classes of these Base Libraries you will be able to implement your own flavor of them, but with much of the common logic pertaining to these efforts already resolved, which was achieved by sticking to the SOLID principles as much as possible.


## Folder structure
### BaseLibs folder
- Contains all the basic libraries from where you can build all the implementations you need to build an *MVP * project architecture, *EF6 Database* layer and Business *Logic* layer, while keeping them all separated. Or if you prefer to just use one of the libraries to take advantage of the usage of a single module.

### Implementations folder
We built a series of implementations as building blocks for each of the Base Libraries we defined earlier, just to make developing effort even easier, you can still derive from this classes and implement your own style of doing things.

## Testing of these base libraries
We are still in the process of building unit tests and POCs that work as tests and examples simultaneously. But we already have some of them in place and we have done extensive use of these libraries to know that they work pretty much good. 
However, everything is perfectible so, if you find any bugs or need a new feature please feel free to report bugs, fork the project and / or send Pull Requests our way!

## License
Just download and use, no strings attached. If this code is useful to you just go ahead and use it. Remember that we provide this code as-is, other than doing our best to fix any bugs and merge PRs we don't extend guarantees for the quality of this code.

# Code contributions
- Windows Forms Collapsible Splitter Control for .Net | (c)Copyright 2002-2003 NJF (furty74@yahoo.com). All rights reserved.

