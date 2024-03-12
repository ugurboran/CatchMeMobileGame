using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IAuth
{
	public bool IsSignedInCompleted { get; set; }
	public string LogMessage { get; set; }
	public void SignIn();
	public void SignOut();
}
