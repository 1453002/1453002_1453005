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
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace DaydreamElements.SwipeMenu {

  public class Balloon : MonoBehaviour {
    /// Amplitude of floating motion for the balloon (meters).
    private const float FLOATING_AMPLITUDE = 0.4f;

    /// Period of floating motion for the balloon (seconds).
    private const float FLOATING_PERIOD = 12.0f;

    /// Amount of time to grow the balloon before popping (seconds).
    private const float POP_TIMER = 0.2f;

    /// Growth of the balloon in scale per second.
    private const float POP_SCALE = 0.8f;

    /// Number of exploded quads to create
    private const int NUM_EXPLODED_QUADS = 100;

    /// Number of seconds for a balloon to fully appear.
    private const float APPEARING_TIME = 0.5f;

    private Vector3 startScale;
    private Vector3 startPosition;
    private float startTime;
    private ColorUtil.Type type;
    private float appearingTimer = 0.0f;
    private bool isPopping = false;
    private bool isAppearing = true;
    public BalloonSpawner spawner;
    public int balloonIx;
        Dictionary<string, GameObject> balloonIns;
    public GameObject explodedQuad;
    public GameObject popSound;
    GameObject answer;
   void Start() {
      startPosition = transform.localPosition;
      startScale = transform.localScale;
      startTime = Time.realtimeSinceStartup + Random.Range(0.0f, FLOATING_PERIOD);
      type = ColorUtil.RandomColor();
      answer= this.gameObject.transform.findChildRecursively("Answer").gameObject;
            //baonh   

            checkCurrentColor((int)type);
            testSwipeColor.instance.balloonIns.Add(this.gameObject.name, this.gameObject);
           // this.gameObject.AddComponent<multipleChoice>();
            GameObject player = GameObject.Find("Player");
            balloonIns = testSwipeColor.instance.balloonIns;
            if (!balloonIns.ContainsKey(this.gameObject.name))
            {
                balloonIns.Add(this.gameObject.name, this.gameObject);
            }
          

      if(balloonIns.ContainsKey("A"))
            {
                balloonIns["A"].transform.findChildRecursively("Text").GetComponent<Text>().text = testSwipeColor.instance.classQuestion.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + testSwipeColor.instance.currentQuestion)).getFieldValue("Option1").stringValue;
            }
            if (balloonIns.ContainsKey("B"))
            {
                balloonIns["B"].transform.findChildRecursively("Text").GetComponent<Text>().text = testSwipeColor.instance.classQuestion.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + testSwipeColor.instance.currentQuestion)).getFieldValue("Option2").stringValue;
            }
            if (balloonIns.ContainsKey("C"))
            {
                balloonIns["C"].transform.findChildRecursively("Text").GetComponent<Text>().text = testSwipeColor.instance.classQuestion.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + testSwipeColor.instance.currentQuestion)).getFieldValue("Option3").stringValue;
            }
            if (balloonIns.ContainsKey("D"))
            {
                balloonIns["D"].transform.findChildRecursively("Text").GetComponent<Text>().text = testSwipeColor.instance.classQuestion.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + testSwipeColor.instance.currentQuestion)).getFieldValue("Option4").stringValue;
            }

            ColorUtil.Colorize(type, gameObject);
      transform.localScale = Vector3.zero;
      float randAngle = Random.Range(0.0f, 360.0f);
      transform.localRotation = Quaternion.Euler(0.0f, randAngle, 0.0f);
            if (this.gameObject.scene.name == "Showroom2_01")
            {
                startScale *= 0.3f;
            }
        }

    //baonh
    void checkCurrentColor(int param)
    {
        if (param == 0) { testSwipeColor.instance.currentColor += 1; this.gameObject.name = "A"; }
        if (param == 1) { testSwipeColor.instance.currentColor += 2; this.gameObject.name = "B"; }
        if (param == 2) { testSwipeColor.instance.currentColor += 4; this.gameObject.name = "C"; }
        if (param == 3) { testSwipeColor.instance.currentColor += 8; this.gameObject.name = "D"; }


        }
    void checkAnswer()
    {
       if(this.gameObject.name.Equals(testSwipeColor.instance.getAnswer(testSwipeColor.instance.currentQuestion)))
       {
                testSwipeColor.instance.score += 10;
       }
       if(testSwipeColor.instance.currentQuestion + 1 <= testSwipeColor.instance.maxQuestion)
       {
                testSwipeColor.instance.currentQuestion += 1;
                testSwipeColor.instance.loadQuestion(testSwipeColor.instance.currentQuestion);
                if (testSwipeColor.instance.currentQuestion == testSwipeColor.instance.maxQuestion) {
                }
       }
    }

    void Update() {
        answer.transform.LookAt(GameObject.Find("PlayerPlay").transform);
        if (isAppearing) {    
        float scale = Mathf.Min(appearingTimer / APPEARING_TIME, 1.0f);
        appearingTimer += Time.deltaTime;
        transform.localScale = startScale * scale;
        if (scale >= 1.0f) {
          isAppearing = false;
        }
        ApplyFloatingAnimation();
      } else if (isPopping) {
        CreateExplosion();
        Instantiate(popSound, transform.position, Quaternion.identity);
        Destroy(gameObject);
      } else {
        ApplyFloatingAnimation();
      }
    }

    void OnDestroy() {
            //SoundResonanceManager.instance.playSfx(gameObject, "Bounce");
      Sign.IncScore();
      if (spawner != null) {
        spawner.BalloonDestroyed(balloonIx);
      if(balloonIns.ContainsKey(this.gameObject.name))
      {
          balloonIns.Remove(this.gameObject.name);
      }
        checkAnswer();
        if((int)type == 0) { testSwipeColor.instance.currentColor -= 1; }
        if((int)type == 1) { testSwipeColor.instance.currentColor -= 2; }
        if ((int)type == 2) { testSwipeColor.instance.currentColor -= 4; }
        if ((int)type == 3) { testSwipeColor.instance.currentColor -= 8; }
            }
    }

    void OnCollisionEnter(Collision collision) {
      RigidPaperAirplane rigidPencil = collision.gameObject.GetComponent<RigidPaperAirplane>();
      if (rigidPencil == null) {
        return;
      }
      if (!rigidPencil.isSpinning && rigidPencil.type == type) {
        Destroy(collision.gameObject);
        isPopping = true;
      } else {
                //rigidPencil.Spin();
                Destroy(collision.gameObject);
                isPopping = true;
            }
    }

    private void ApplyFloatingAnimation() {
      float t = startTime - Time.realtimeSinceStartup;
      float w = (2 * Mathf.PI / FLOATING_PERIOD);
      float delta = Mathf.Sin(t * w) * FLOATING_AMPLITUDE;
      transform.localPosition = startPosition + Vector3.up * delta;
    }

    private void CreateExplosion() {
      SphereCollider collider = GetComponent<SphereCollider>();
      Vector3 center = transform.localToWorldMatrix.MultiplyPoint3x4(collider.center);
      for (int i = 0; i < NUM_EXPLODED_QUADS; ++i) {
        GameObject quad = Instantiate(explodedQuad);
        Vector3 delta = Random.onUnitSphere * 1.5f;
        float sx = Random.Range(0.1f, 0.5f);
        float sy = Random.Range(0.1f, 0.5f);
        quad.transform.position = center + delta;
        quad.transform.rotation = Quaternion.FromToRotation(Vector3.forward, delta);
        quad.transform.localScale = new Vector3(sx, sy, 1.0f);
        Rigidbody rigidBody = quad.GetComponent<Rigidbody>();
        float ax = Random.Range(-1.0f, 1.0f);
        float ay = Random.Range(-1.0f, 1.0f);
        float az = Random.Range(-1.0f, 1.0f);
        rigidBody.angularVelocity = new Vector3(ax, ay, az);
        rigidBody.velocity = delta * 3.0f;
        Color color = ColorUtil.ToColor(type) * Random.Range(0.5f, 1.0f);
        ColorUtil.Colorize(color, quad);
      }
    }
  }
}
