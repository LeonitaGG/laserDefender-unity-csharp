using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GameplayTests_01
    {

        // A Test behaves as an ordinary method
        [Test]
        public void GameplayTests_01SimplePasses() //Example test layout
        {
            //Arrange
            var playerGameObject = new GameObject(); //declare new GameObject

            //Act
            var newPlayerName = "Player";
            playerGameObject.name = newPlayerName;

            //Assert
            Assert.AreEqual(newPlayerName, playerGameObject.name);
        }

        /// <summary>
        /// 
        /// </summary>

        private const int StartingHealth = 100;
        private PlayerHealth playerHealth;
        [Test]
        public void Player_Health_Is_Set()
        {
            playerHealth = new PlayerHealth(StartingHealth);

            Assert.AreEqual(100, playerHealth.Health);

        }

        [Test]
        public void Player_Ship_Is_Destroyed()
        {
            playerHealth = new PlayerHealth(StartingHealth);
            var enemyLaserGameObject = new GameObject();

            //
            var enemyLaser = enemyLaserGameObject.AddComponent<DamageDealer>();
            playerHealth.RemoveHealth(enemyLaser.GetDamage());

            //
            Assert.IsTrue(playerHealth.IsDestroyed);
        }


        /// <summary>
        /// 
        /// </summary>

        [Test]
        public void Player_Health_Decreases_When_Hit_By_Enemy_Laser()
        {
            //Arrange
            var playerGameObject = new GameObject(); //create new GameObject
            //var playerObject = UnityEngine.Object.Instantiate(playerGameObject, Vector2.zero, Quaternion.identity) as GameObject; //*
            var enemyLaserGameObject = new GameObject();
            var playerColliderObject = new GameObject();

            //Act
            //var player = playerGameObject.AddComponent<Player>(); //Add the behaviour of the "Player" class via "AddComponent" 
            var player = playerGameObject.AddComponent<Player>(); //*

            var playerCollider = playerColliderObject.AddComponent<CircleCollider2D>();
            var enemyLaser = enemyLaserGameObject.AddComponent<DamageDealer>(); 

            int initialPlayerHealth = player.GetHealth(); // =200 by default 

            playerCollider.transform.position = enemyLaser.transform.position;
            //player.transform.position = enemyLaser.transform.position; //set the enemy laser to collide with the player collision

            //Assert
            Assert.Less(playerCollider.GetComponent<Player>().GetHealth(), initialPlayerHealth);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator GameplayTests_01WithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
