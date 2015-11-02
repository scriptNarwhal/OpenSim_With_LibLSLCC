Welcome to OpenSim!

# LibLSLCC Integration

This distribution of OpenSim integrates the LibLSLCC OpenSim LSL Compiler
as an optional compiler that can be used instead of the default compiler inside
XEngine.

See:

	https://github.com/EriHoss/LibLSLCC

	Or
	
	https://gitlab.com/erihoss/LibLSLCC


Settings that determine the compiler Assembly (dll) and class have been added under the
[XEngine] configuration section in OpenSim.ini.  See OpenSim.ini in the 'bin' folder where the OpenSim
executable resides.

The included OpenSim.ini.example and OpenSim.ini have the default compiler assembly
set to the "LibLSLCCCompiler.dll" so that LibLSLCC is used as the default compiler.



# Configuration I Have Added


Under [XEngine] in OpenSim.ini you will find these new settings:


	; ==========================================
	; LibLSLCC Patch Settings
	; ==========================================
	
	; The name of the class that implements the compiler
	; CompilerClass = "OpenSim.Region.ScriptEngine.Shared.CodeTools.Compiler"
	  CompilerClass = "OpenSim.Region.ScriptEngine.Shared.LibLSLCC.Compiler"
	
	; The assembly to load the compiler implementation from
	; CompilerAssembly = "OpenSim.Region.ScriptEngine.Shared.CodeTools.dll"
	  CompilerAssembly = "OpenSim.Region.ScriptEngine.Shared.LibLSLCC.dll"
	
	;============================================

When you clone this repository OpenSim is pre-configured in standalone mode using the SQLite storage backend.


OpenSim.ini and bin/config-include/StandaloneCommon.ini have been left in the repository
for this distribution to make running a test server on your PC easier.  If you just want
to run a test server on localhost, you should not have to modify any configuration after building,
just run OpenSim.exe and set up your region and region owner by answering the questions OpenSim
asks you in the command prompt.



# OpenSim Changes (IScriptModuleComms)


I have modified the IScriptModuleComms interface to return more information about the origin of registered script constants.

The 'ScriptModuleCommsModule' implementation has been updated to support this new interface.


IScriptModuleComms.GetConstants() has been modified as so:


```C#

	Dictionary<string, object> GetConstants();
	
	//is now:

	Dictionary<string, ScriptConstantInfo> GetConstants();
	
```	
	

'ScriptConstantInfo' contains the reflected value of the constant from the static field/property 
it was defined with, as well as a 'MemberInfo' object for the member that caused it to be defined.
This is so you can reflect attributes off the 'MemberInfo' if needed.  

LibLSLCC uses this with its attribute framework to generate library data for registered module constants
that it can use for syntax checking.  

LibLSLCC was already able generate library data for methods/script invocations without any changes to the 'IScriptModuleComms' interface.
This is because the 'Delegate' class has a property named 'Method' which points to the class method the delegate was created from.
Attributes from the original class method are reflected off 'Delegate.Method'.

These delegate's are taken directly from 'IScriptModuleCommsGetScriptInvocationList()' and library data is generated for each method.


ScriptConstantInfo's public members are defined as:


```C#

    public class ScriptConstantInfo
    {
        /// <summary>
        /// Gets the class member that represents the constant.
        /// Could be a field or property.
        /// </summary>
        /// <value>
        /// The class member that represents the constant.
        /// </value>
        public MemberInfo ClassMember { get; private set; }

        /// <summary>
        /// Gets the constants value, taken from the class member that represents it.
        /// </summary>
        /// <value>
        /// The constants value, taken from the class member.
        /// </value>
        public object ConstantValue { get; private set; }
    }
	
```


LookupModConstant now also returns a ScriptConstantInfo object as well, instead of just the constants value:


```C#

	object LookupModConstant LookupModConstant();

	//is now:

	ScriptConstantInfo LookupModConstant(string cname);
	
```




RegisterConstant(string cname, object value); has been removed as it was not used in any module that comes with OpenSim by default,
and did not support the idea behind the new interface:


```C#

	//Removed:
	
	void RegisterConstant(string cname, object value);
	
```

	
All refactorings have been made to make this new interface work with the old OpenSim
compiler, there was only one change made to the old compiler.

It was for one call to: LookupModConstant(string cname);



# OpenSim Changes (Module/ScriptBaseClass Attributes)



Each core/optional module containing script invocations, as well as ScriptBaseClass
have been given attributes from LibLSLCC's method/constant namespace LibLSLCC.LibraryData.Reflection.

This is so LibLSLCC can use its built in class serializer to generate library data for loaded modules
instead of loading it off the disk from a seperate configuration file.


None of the old [ScriptConstant] and [ScriptConstantInfo] attributes were removed
and the IScriptModuleComms implementation ScriptModuleCommsModule will still detect them.


ScriptModuleCommsModule now also detects attributes from LibLSLCC's 
LibLSLCC.LibraryData.Reflection namespace as well as the old [ScriptConstant] and [ScriptConstantInfo]
attributes.


LibLSLCC has the ability to use 'type converters' in its serializer to convert the types
of a .NET signature (either a property/field or method) into a corrisponding LSLType that
is consumeable by the library,  however I chose to attribute each constant, method and parameter
of every module explicitly with LibLSLCC attributes.

I think it is better for the serializer not to be guessing for modules that are provided with 
OpenSim.


======


LibLSLCC's attribute system allows a class to provide its own type converter for when you
do not want to explicitly attribute each method.

Each [LSLFunctionAttribute] and [LSLConstantAttribute] can also provide a prefered type converter
for the field/property or method they are applied to.


If no type converter is found, the job of type converting the types in a field/property or signature
falls back to the LSLLibraryDataReflectionSerializer used in the compiler.
(OpenSim.Region.ScriptEngine.Shared.LibLSLCCCompiler.Compiler)

The serializer does not define any converters so it will throw an exception if the module does not
define one and types need to be converted because the types in a signature were not explicitly attributed.


The serializer will only attempt to serialize fields/properties and methods with an [LSLConstantAttribute]
or [LSLFunctionAttribute]. currently, the serializer's settings specify that parameters that are not attributed
with [LSLParamAttribute] are to be left out of the signature generated for library data. 

This is so the first two parameters of each module script invocation can be left out of the signature 
used by LibLSLCC for syntax checking, since they are called via a generated delegate and a modInvoke*
function call.




# Overview

OpenSim is a BSD Licensed Open Source project to develop a functioning
virtual worlds server platform capable of supporting multiple clients
and servers in a heterogeneous grid structure. OpenSim is written in
C#, and can run under Mono or the Microsoft .NET runtimes.

This is considered an alpha release.  Some stuff works, a lot doesn't.
If it breaks, you get to keep *both* pieces.

# Compiling OpenSim

Please see BUILDING.md if you downloaded a source distribution and 
need to build OpenSim before running it.

# Running OpenSim on Windows

You will need .NET 4.5 installed to run this distribution of OpenSimulator.

We recommend that you run OpenSim from a command prompt on Windows in order
to capture any errors.

To run OpenSim from a command prompt

 * cd to the bin/ directory where you unpacked OpenSim
 * run OpenSim.exe

Now see the "Configuring OpenSim" section

# Running OpenSim on Linux

You will need Mono >= 2.10.8.1 to run OpenSimulator.  On some Linux distributions you
may need to install additional packages.  See http://opensimulator.org/wiki/Dependencies
for more information.

To run OpenSim, from the unpacked distribution type:

 * cd bin
 * mono OpenSim.exe

Now see the "Configuring OpenSim" section

# Configuring OpenSim

When OpenSim starts for the first time, you will be prompted with a
series of questions that look something like:

	[09-17 03:54:40] DEFAULT REGION CONFIG: Simulator Name [OpenSim Test]:

For all the options except simulator name, you can safely hit enter to accept
the default if you want to connect using a client on the same machine or over
your local network.

You will then be asked "Do you wish to join an existing estate?".  If you're
starting OpenSim for the first time then answer no (which is the default) and
provide an estate name.

Shortly afterwards, you will then be asked to enter an estate owner first name,
last name, password and e-mail (which can be left blank).  Do not forget these
details, since initially only this account will be able to manage your region
in-world.  You can also use these details to perform your first login.

Once you are presented with a prompt that looks like:

	Region (My region name) #

You have successfully started OpenSim.

If you want to create another user account to login rather than the estate
account, then type "create user" on the OpenSim console and follow the prompts.

Helpful resources:
 * http://opensimulator.org/wiki/Configuration
 * http://opensimulator.org/wiki/Configuring_Regions

# Connecting to your OpenSim

By default your sim will be available for login on port 9000.  You can login by
adding -loginuri http://127.0.0.1:9000 to the command that starts Second Life
(e.g. in the Target: box of the client icon properties on Windows).  You can
also login using the network IP address of the machine running OpenSim (e.g.
http://192.168.1.2:9000)

To login, use the avatar details that you gave for your estate ownership or the
one you set up using the "create user" command.

# Bug reports

In the very likely event of bugs biting you (err, your OpenSim) we
encourage you to see whether the problem has already been reported on
the [OpenSim mantis system](http://opensimulator.org/mantis/main_page.php).

If your bug has already been reported, you might want to add to the
bug description and supply additional information.

If your bug has not been reported yet, file a bug report ("opening a
mantis"). Useful information to include:
 * description of what went wrong
 * stack trace
 * OpenSim.log (attach as file)
 * OpenSim.ini (attach as file)
 * if running under mono: run OpenSim.exe with the "--debug" flag:

       mono --debug OpenSim.exe

# More Information on OpenSim

More extensive information on building, running, and configuring
OpenSim, as well as how to report bugs, and participate in the OpenSim
project can always be found at http://opensimulator.org.

Thanks for trying OpenSim, we hope it is a pleasant experience.


