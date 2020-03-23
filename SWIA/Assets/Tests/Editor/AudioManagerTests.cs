using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerTests
{
    [Test]
    public void hasAwoken()
    {
        var test = new GameObject().AddComponent<AudioManager>();
        
        if (AudioManager.instance != null)
        {
            test.Awake();

            Assert.AreEqual(AudioManager.instance, test);

            foreach (Sound s in test.sounds)
            {
                Assert.AreEqual(s.source.clip, s.clip);
                Assert.AreEqual(s.source.loop, s.loop);
                Assert.AreEqual(s.source.volume, s.volume);
                Assert.AreEqual(s.source.pitch, s.pitch);
            }
        }
    }
}
