using UnityEngine;

public class Timer {

	public float cycle;
	public float currentTime;
	public bool active;
	public bool delay;
	public float delayTime;
	
	public Timer(float length){
		this.cycle = length;
		this.active = false;
		this.delay = false;
		this.delayTime = 0.0f;
		this.currentTime = 0.0f;
	}

	public void Update(){
		if(this.active)
			this.currentTime += Time.deltaTime * Time.timeScale;
		
		if(delay && this.currentTime >= this.delayTime)
			delay = false;
	}

	public bool Complete(){
		return active && this.currentTime >= this.cycle && !this.delay;
	}

	public void Start(){
		this.active = true;
	}

	public void Restart(){
		this.currentTime = 0.0f;
	}

	public void Pause(){
		this.active = false;
	}

	public void Stop(){
		this.active = false;
	}

	public void SetDuration(float len){
		this.cycle = len;
	}
	
	public void DelayedStart(float delayedTime){
		this.delayTime = delayedTime;
		this.delay = true;
		this.Start();
	}	
}
