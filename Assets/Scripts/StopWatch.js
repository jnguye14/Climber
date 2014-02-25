#pragma strict

// Note: this script requires a guiText Component

public var isStopped : boolean = false;
public var currentTime : float;

function Start ()
{
	currentTime = 0.0f;
	guiText.text = currentTime.ToString();
}

function Update ()
{
	if(!isStopped)
	{
		currentTime += Time.deltaTime;
		guiText.text = TimeToString();
	}
}

function RestartTimer()
{
	currentTime = 0.0f;
	guiText.text = TimeToString();
}

function ResumeTimer()
{
	isStopped = false;
}

function PauseTimer()
{
	isStopped = true;
}

function TimeToString()
{
	var seconds : int = Mathf.FloorToInt(currentTime);
	var milliseconds : float = currentTime - seconds;

	var toReturn : String = ":";
	var temp : String = milliseconds.ToString();
	if(temp.Length > 2)
	{
		toReturn += temp.Substring(2,Mathf.Abs(temp.Length-2));
	}
	else
	{
		toReturn += "0";
	}
	
	if(seconds < 10)
	{
		toReturn = "0" + seconds.ToString() + toReturn;
	}
	else if(seconds < 60)
	{
		toReturn = seconds.ToString() + toReturn;
	}
	else // seconds > one minute
	{
		var minutes : int = seconds / 60;
		seconds = seconds % 60;
		
		if(seconds < 10)
		{
			toReturn = ":0" + seconds.ToString() + toReturn;				
		}
		else
		{
			toReturn = ":" + seconds.ToString() + toReturn;
		}
		
		if(minutes < 60)
		{
			toReturn = minutes.ToString() + toReturn;
		}
		else // minutes > one hour
		{
			var hours : int = minutes / 60;
			minutes = minutes % 60;
			
			if(minutes < 10)
			{
				toReturn = hours.ToString() + ":0" + minutes.ToString() + toReturn;
			}
			else
			{
				toReturn = hours.ToString() + ":" + minutes.ToString() + toReturn;
			}
		}
	}
	
	return toReturn;
}