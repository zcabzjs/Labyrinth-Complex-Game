using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class KeyManagerTestScript {

    [Test]
    public void KeyManager_Not_Null_Test() {
        // Use the Assert class to test conditions.
        var keyManager = GameObject.Find("KeyManager");
        Assert.AreNotEqual(null, keyManager);
    }

    [Test]
    public void KeyManager_GenerateKeys_Not_Zero_Test()
    {
        var keyManager = GameObject.Find("KeyManager").GetComponent<KeyManager>();
        keyManager.GenerateKeys();
        Assert.AreNotEqual(0, keyManager.initialKeyArray.Count);
    }

    [Test]
    public void KeyManager_GenerateKeys__All_Alphanumeric_Test()
    {
        var keyManager = GameObject.Find("KeyManager").GetComponent<KeyManager>();
        keyManager.GenerateKeys();
        var testKeyArray = keyManager.initialKeyArray;

        const string upperCaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string numericCharacters = "0123456789";

        string mixedCharacters = upperCaseCharacters + numericCharacters;

        bool valid = true;
        for(int i = 0; i < testKeyArray.Count; i++)
        {
            if (!mixedCharacters.Contains(testKeyArray[i]))
            {
                valid = false;
            }
        }

        Assert.AreEqual(true, valid);
    }

}
