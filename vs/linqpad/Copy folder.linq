<Query Kind="Statements" />

var sourcePath = @"C:\temp\source";
var targetPath = @"C:\temp\target";

// Create all the directories
foreach(var dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
{
	Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
}

// Copy all the files and replace any files with the same name
foreach(var newPath in Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories))
{
	File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
}