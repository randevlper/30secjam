using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gold
{
    namespace Delegates
    {
        public delegate void ValueChange<T>(T value);
        public delegate void Inform();
    }

    public class Timer
    {
        public float length;
        public System.Action methodToCall;
        float _time;
        public bool isLooping;

        bool isTicking;
        public bool IsTicking
        {
            get { return isTicking; }
        }

        public float RemainingTime
        {
            get{return _time;}
        }

        public Timer(System.Action action, float time, bool loop = false)
        {
            methodToCall = action;
            length = time;
            isLooping = loop;
        }

        public void Tick(float delta)
        {
            if (isTicking)
            {
                _time -= delta;

                if (_time < 0)
                {
                    methodToCall();
                    if (isLooping)
                    {
                        Reset();
                    }
                    else
                    {
                        Stop();
                    }
                }
            }
        }

        public void Start()
        {
            Reset();
            Resume();
        }

        public void Reset()
        {
            _time = length;
        }

        public void Resume()
        {
            isTicking = true;
        }

        public void Stop()
        {
            isTicking = false;
        }
    }

    [System.Serializable]
    public class ObjectPool
    {
        GameObject _objectToPool;
        Stack<GameObject> _pooledObjects;
        public int pooledAmount;
        public bool canGrow;

        public ObjectPool(GameObject obj, int num, bool grow = false)
        {
            _pooledObjects = new Stack<GameObject>();
            pooledAmount = num;
            _objectToPool = obj;
            canGrow = grow;

            for (int i = 0; i < pooledAmount; i++)
            {
                SpawnObj();
            }
        }

        //Need to check if this is the proper gameObject
        void Add(GameObject obj)
        {
            _pooledObjects.Push(obj);
        }

        public GameObject Get()
        {
            if (_pooledObjects.Count <= 0)
            {
                if (canGrow)
                {
                    SpawnObj();
                    pooledAmount++;
                    return _pooledObjects.Pop();
                }
                else
                {
                    return null;
                }
            }

            return _pooledObjects.Pop();
        }

        void SpawnObj()
        {
            GameObject spawnedObj = GameObject.Instantiate(_objectToPool, Vector3.zero, Quaternion.identity);
            spawnedObj.SetActive(false);
            spawnedObj.AddComponent<ObjectPoolComponent>().Setup(this, Add);
            _pooledObjects.Push(spawnedObj);
        }

        //Can only have one GameObject to pool
        //Can optionally have a number of objects pooled initialy
        //Option to grow
        //Option for max number of pooled items

        //Objects are sent back to the stack when they are done
    }

    [System.Serializable]
    public class StateMachineAction
    {
        public System.Action OnEnter;
        public System.Action OnStay;
        public System.Action OnExit;

        StateMachineAction() { }

        public StateMachineAction(System.Action onStay, System.Action onEnter = null, System.Action onExit = null)
        {
            OnEnter += onEnter;
            OnStay += onStay;
            OnExit += onExit;

            OnEnter += Empty;
            OnStay += Empty;
            OnExit += Empty;
        }

        void Empty() { }
    }

    [System.Serializable]
    public class StateMachine
    {
        StateMachineAction _currentState;
        Dictionary<string, StateMachineAction> _states;
        bool isRunning;

        public StateMachine()
        {
            _states = new Dictionary<string, StateMachineAction>();
        }

        public bool Start(string key)
        {
            StateMachineAction retval;
            if (_states.TryGetValue(key, out retval))
            {
                _currentState = retval;
                isRunning = true;
                return true;
            }
            Debug.LogError("WARNING - " + key + " does not exsist! Cannot start the StateMachine!");
            return false;
        }

        public void Tick()
        {
            if(isRunning)
            {
                _currentState.OnStay();
            }
        }

        public bool Add(StateMachineAction state, string key)
        {
            StateMachineAction retval;
            if (_states.TryGetValue(key, out retval))
            {
                return false;
            }
            else
            {
                _states.Add(key, state);
                return true;
            }
        }

        public bool ChangeState(string key)
        {
            StateMachineAction retval;
            if (_states.TryGetValue(key, out retval))
            {
                _currentState.OnExit();
                _currentState = retval;
                _currentState.OnEnter();
                return true;
            }
            return false;
        }
    }
}
