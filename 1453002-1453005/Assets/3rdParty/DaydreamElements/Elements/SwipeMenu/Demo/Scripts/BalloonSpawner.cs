// Copyright 2017 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;

namespace DaydreamElements.SwipeMenu {

  public class BalloonSpawner : MonoBehaviour {
    private const int TARGET_NUM_BALLOONS = 4;
    private int numBalloons = 0;
    private GameObject[] balloons = new GameObject[TARGET_NUM_BALLOONS];
    public GameObject balloonPrefab;
    void Update() {
      // Initialize all balloons
      for (int i = 0; i < TARGET_NUM_BALLOONS; ++i) {
                if (balloons[i] == null)
                {
                    SpawnBalloon(i);
                }
          
      }
    }
    private void OnEnable()
    {
            numBalloons = 0;
    }
    private void SpawnBalloon(int balloonIx) {
      GameObject balloonSpawn = Instantiate(balloonPrefab);
      balloonSpawn.transform.position = new Vector3(Random.Range(-6f,6f),Random.Range(2.41f,6.56f),Random.Range(8.57f,11.73f));
      balloonSpawn.GetComponent<Balloon>().spawner = this;
      balloonSpawn.GetComponent<Balloon>().balloonIx = balloonIx;
      balloons[balloonIx] = balloonSpawn;
    }

    public void BalloonDestroyed(int balloonIx) {
      balloons[balloonIx] = null;
      numBalloons--;
    }
  }
}