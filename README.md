# brainfuck
Yet another Brainfuck interpreter. Written in C#

Yep, that is another bf interpreter. There are millions of them, but now there is mine too.<br>
Brainfuck wikipedia page: https://wikipedia.org/wiki/Brainfuck<br>

Usage:
<ol>
    <li>Download and install git</li>
    <li>Run "git clone https://github.com/kotanys/brainfuck/"</li>
    <li>Compile code with any C# compiler (e.g. OmniSharp)</li>
    <li>Run "SharpBrainfuck.exe {path to file with brainfuck code} [-i] [-l [path]]"</li>
    <ul>
        <li>Arguments</li>
        <ul>
          <li>-i - ignore code check</li>
          <li>-l [path] - Create a log if program crashes. Unless "path" is specified, log.txt will be used as a log file.</li>
          <li>-nc - do not use crash points (`)</li>
        </ul> 
        <li>Note that path must be written as-is, without any additional symbols (like ")</li>
    </ul>
</ol>

Now it supports crash points. If interpreter reaches character \`, program will crash and log will be made.

By the way, you can compile it as .dll and use it in your programs. Just set "OutputType" in SharpBrainfuck.csproj to "library".
  
P.S. I know my code sucks<br>
P.P.S. So does my English
