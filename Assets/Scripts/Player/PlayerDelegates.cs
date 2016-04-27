using UnityEngine;
using System.Collections;
using Shooter.Player;

namespace Shooter.Player.Delegate
{
	public delegate void PlayerHpChange(bool isAdd);
	public delegate void GetItem(GameObject itemObj);
};
