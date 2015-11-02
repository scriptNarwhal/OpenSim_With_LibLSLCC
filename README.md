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


There is a new section in OpenSim.ini for LibLSLCC related settings called [LibLSLCC].
currently the only option is for turning extended compiler warnings on and off:

	[LibLSLCC]
		; Disable or enable LibLSLCC extended compiler warnings.
		; This is set to true by default, but you can set it to false if you think
		; LibLSLCC's extended warnings are too pedantic.
		;
		; In the future I will probably implement warning codes that can be disabled
		; selectively from this configuration file.
		EnableCompilerWarnings = true



Under [XEngine] in OpenSim.ini you will find these new settings:


	; ==========================================
	; LibLSLCC Patch Settings
	; ==========================================
	
	; The name of the class that implements the compiler
	; Default is OpenSim.Region.ScriptEngine.Shared.CodeTools.Compiler
	;
	; CompilerClass = "OpenSim.Region.ScriptEngine.Shared.CodeTools.Compiler"
	  CompilerClass = "OpenSim.Region.ScriptEngine.Shared.LibLSLCCCompiler.Compiler"
	
	; The assembly to load the compiler implementation from
	; Default is OpenSim.Region.ScriptEngine.Shared.CodeTools.dll
	;
	; CompilerAssembly = "OpenSim.Region.ScriptEngine.Shared.CodeTools.dll"
	  CompilerAssembly = "OpenSim.Region.ScriptEngine.Shared.LibLSLCCCompiler.dll"
	
	;============================================

When you clone this repository OpenSim is pre-configured in standalone mode using the SQLite storage backend.


OpenSim.ini and bin/config-include/StandaloneCommon.ini have been left in the repository
for this distribution to make running a test server on your PC easier.  If you just want
to run a test server on localhost, you should not have to modify any configuration after building,
just run OpenSim.exe and set up your region and region owner by answering the questions OpenSim
asks you in the command prompt.


# Dependencies I Have Added


The LibLSLCC Library depends on the CSharp ANTLR 4.5.1 Runtime, and thus it has been
included with this distribution of OpenSim as a pre-built binary in the "bin" folder.

You can find the license for ANTLR4 under the "ThirdPartyLicenses" folder in the 
OpenSim build directory.


For more information about the ANTLR parser generator, go here: 

http://www.antlr.org/index.html



# New Build Project, LibLSLCCCompiler


The location for the new compiler project that does all
the work is *"/OpenSim/Region/ScriptEngine/Shared/LibLSLCCCompiler"*

XEngine has been changed so that it can dynamically load a compiler Assembly and 
Compiler class specified by your OpenSim.ini.


The New compiler compiles to the assembly: **OpenSim.Region.ScriptEngine.Shared.LibLSLCCCompiler.dll**

And the Old compiler compiles to the assembly: **OpenSim.Region.ScriptEngine.Shared.CodeTools.dll**


Both compiler assemblies contain a class named **Compiler** that implements 
**OpenSim.Region.ScriptEngine.Interfaces.ICompiler**.  

You can configure OpenSim.ini to use either one of these implementations
under the **[XEngine]** configuration section.


# OpenSim Changes (IScriptModuleComms)


I have modified the **IScriptModuleComms** interface to return more information about the origin of registered script constants.

The **ScriptModuleCommsModule** implementation has been updated to support this new interface.


**IScriptModuleComms.GetConstants()** has been modified as so:


```C#

	Dictionary<string, object> GetConstants();
	
	//is now:

	Dictionary<string, ScriptConstantInfo> GetConstants();
	
```	
	

**ScriptConstantInfo** contains the reflected value of the constant from the static field/property 
it was defined with, as well as a **MemberInfo** object for the member that caused it to be defined.
This is so you can reflect attributes off the **MemberInfo** if needed.  

LibLSLCC uses this with its attribute framework to generate library data for registered module constants
that it can use for syntax checking.  

LibLSLCC was already able to generate library data for methods/script invocations without any changes to the **IScriptModuleComms** interface.
This is because the **Delegate** class has a property named **Method** which points to the class method the delegate was created from.
Attributes from the original class method are reflected off **Delegate.Method**.

These delegate's are taken directly from **IScriptModuleComms.GetScriptInvocationList()** and library data is generated for each method.


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


**IScriptModuleComms.LookupModConstant** now also returns a **ScriptConstantInfo** object as well, instead of just the constants value:


```C#

	object LookupModConstant(string cname);

	//is now:

	ScriptConstantInfo LookupModConstant(string cname);
	
```




**IScriptModuleComms.RegisterConstant** has been removed as it was not used in any module that comes with OpenSim by default,
and did not support the idea behind the new interface:


```C#

	//Removed:
	
	void RegisterConstant(string cname, object value);
	
```

	
All refactoring’s have been made to make this new interface work with the old OpenSim
compiler, there was only one change made to the old compiler.

It was for a single call to: **LookupModConstant(string cname);**



# OpenSim Changes (Module/ScriptBaseClass Attributes)



Each core/optional module containing script invocations, as well as **ScriptBaseClass**
have been given attributes from LibLSLCC's method/constant namespace **LibLSLCC.LibraryData.Reflection**.

This is so LibLSLCC can use its built in class serializer to generate library data for OpenSim's base LSL implementation 
as well as loaded modules;  instead of loading it off the disk from a separate configuration file.


None of the old **[ScriptConstant]** and **[ScriptConstantInfo]** attributes were removed,
and the **IScriptModuleComms** implementation **ScriptModuleCommsModule** will still detect them.


However, **ScriptModuleCommsModule** now also detects attributes from LibLSLCC's **LibLSLCC.LibraryData.Reflection** namespace 
as well as the old **[ScriptConstant]** and **[ScriptConstantInfo]** attributes.  Either or both attributes can be used, but
if you want the LibLSLCC Compiler to see your module method/constant, you should make sure to attribute it with the attributes from
**LibLSLCC.LibraryData.Reflection**.


LibLSLCC has the ability to use 'type converters' in its class serializer to convert the types
in a .NET signature (either a property/field or method) into a corresponding **LSLType** that
is consumable by the library.  


However, I chose to attribute each constant, method and parameter of **ScriptBaseClass** and the modules
provided with OpenSim explicitly with LibLSLCC attributes.

I think it is better for the class serializer not to be using type converters at the serializer level.

Using type converters at the serializer level would limit module implementors to using a certain set of types for constants and method parameters.
They may want to implement utility types with all the correct conversions to be compatible with the LSL Runtime types for example.

Module implementors however can implement their own type converters, and supply their **Type** to the 
class level **LSLLibraryDataSerializableAttribute** from **LibLSLCC.LibraryData.Reflection**.

They can also supply them to the  member level attributes **LSLFunctionAttribute** and **LSLConstantAttribute**.

Converters supplied to class/member level attributes will indeed be honored by the class serializer and used to
convert the Types used within the class or specific to a certain method/constant.  This is okay since the person 
writing the module is the one who decides how .NET types convert into corresponding **LSLType's**.



# More About LibLSLCC Attributes


LibLSLCC's attribute system allows a class to provide its own type converters for when you
do not want to explicitly attribute each method.  You can also explicitly set the type converters
used per method/constant.

Constants require a ValueStringConverter to convert their raw value into a string that 
**LSLLibraryFunctionSignature.ValueString** can parse for the given type, the serializer in the compiler
sets a default one that just (ToString()'s) the constant value.  

This works for OpenSim, OpenMetaverse and CSharp built in types that may be used to define a constant, 
since **LSLLibraryFunctionSignature.ValueString** is able to parse their (ToString()) output.  

But module implementors may need to implement their own ValueStringConverter if they are using strange types to define constants.


Type converters are required to implement the interface: **LibLSLCC.LibraryData.Reflection.ILSLTypeConverter**

And ValueStringConverter's are required to implement the interface: **LibLSLCC.LibraryData.Reflection.ILSLValueStringConverter**


Here are some examples of the Attributes in use:


```C#


//
// You can explicitly attribute the constant...
//
[LSLConstant(LSLType.Integer))]
public const int MY_CONSTANT = 42;


//
// Or use the ILSLTypeConverter implementation "MyTypeConverter" to convert the field
// type of this specific field into the corresponding "LSLType".
//
[LSLConstant(TypeConverter = typeof(MyTypeConverter))]
public const int MY_CONSTANT = 42;



//
// Optionally you can set a value string converter for your type, to convert the value
// of the field into a value that can be assigned to LSLLibraryFunctionSignature.ValueString.
//
// The LSLLibraryFunctionSignature.ValueString is format checked, and will throw an exception
// if an improper value string is supplied for the constant type.
//
// The serializer in the compiler has the default ValueStringConverter implementation set to 
// an object that simply calls 'ToString()' on the value.  
//
// Since LSLLibraryFunctionSignature.ValueString can parse the 'ToString()' output of
// every OpenSim type used as a constant so far in OpenSim's code base (including Vectors and Rotations),
// its not a problem for the existing code.
//
// However, if a module implementor wants to use some strange type as an LSL constant, they will need to implement
// the 'LibLSLCC.LibraryData.Reflection.ILSLValueStringConverter' interface, and pass the implementation 
// to the [LSLConstantAttribute] in the named parameter 'ValueStringConverter'.
//
[LSLConstant(LSLType.Integer, ValueStringConverter = typeof(MyValueStringConverter))]
public const int MY_CONSTANT = 42;



//
// You can also just explicitly specify the value string as "42"
//
// LSLLibraryFunctionSignature.ValueString's parsing rules are still in effect.
//
[LSLConstant(LSLType.Integer, ValueString = "42")]
public const int MY_CONSTANT = 42;



```

And for script invocations (methods):


```C#


//
// You can explicitly attribute everything...
//
[LSLFunction(LSLType.Integer))]
int hello_world([LSLParam(LSLType.String)] string param){

}


//
// Or use the ILSLTypeConverter implementation "MyReturnTypeConverter" to convert the parameter
// types of this specific method into their corresponding "LSLType".
//
[LSLFunction(LSLType.Integer, ParamTypeConverter = typeof(MyParamTypeConverter))]
int hello_world(string param){

}

//
// Or use it to convert the return type, and explicitly attribute the parameters
//
[LSLFunction(ReturnTypeConverter = typeof(MyParamTypeConverter))]
int hello_world([LSLParam(LSLType.String)] string param){

}


//
// Or use it to convert all types
//
[LSLFunction(
	ReturnTypeConverter = typeof(MyParamTypeConverter), 
	ParamTypeConverter = typeof(MyParamTypeConverter))]
int hello_world(string param){

}


```



At the class level (IE, module level in OpenSim's case) you can use the **[LSLLibraryDataSerializable]**:

```C#


//
// You can set type/ValueString converter implementations class wide, and they will only be overridden
// if the field/property/method explicitly overrides them. 
//
[LSLLibraryDataSerializable(
	ReturnTypeConverter = typeof(MyReturnTypeConverter),     //Set the default return type converter for every method in the class
	ParamTypeConverter = typeof(MyParamTypeConverter),       //Set the default parameter type converter for every parameter of method's in the class
	ConstantTypeConverter = typeof(MyConstantTypeConverter), //Set the default type converter for every field/property in the class
	ValueStringConverter = typeof(MyValueStringConverter)	 //Set the default ValueStringConverter for every field/property in the class
)]
public class MyModule {



}

```



Additionally, The first two parameters of each Attributed module script invocation are left 
out of the de-serialized signature that is used by LibLSLCC for syntax checking.  


This is accomplished in the new compiler with the use of **LSLLibraryDataReflectionSerializer.ParameterFilter**.


```C#

reflectionSerializer.ParameterFilter = parameterInfo => parameterInfo.Position < 2; 

```


A lambda is used to set **LSLLibraryDataReflectionSerializer.ParameterFilter** to a function that
returns **true** for parameters with a Position less than 2.  Take note that **ParameterInfo.Position** is a Zero based index.


This is required because registered module functions are called via a modInvoke*
function call, which fills out the first two parameters with the **hostID** and **scriptID**.



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

You will need .NET 4.0 installed to run OpenSimulator.

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


