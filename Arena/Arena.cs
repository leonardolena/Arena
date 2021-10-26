using Arena.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ArenaApplication
{
    public class Arena
    {

        public Arena() { }

        private bool GladiatorRequest => _queue.Count > 0;
        public bool HasStarted;
        public volatile bool IsStopped;
        public bool UserAction;
        private Queue<Fighter> _queue;
        public void Stop() {
            UserAction=true;
            IsStopped = true;
            _queue.Clear();
        }
        public void Request(Fighter g) {
            _queue.Enqueue(g);
        }

        public void Cancel() {
            _queue.Clear();
            HasStarted = false;
        }
        public void Recover(List<Fighter> gladiators) {
            foreach (var g in gladiators) _queue.Enqueue(g);
        }
        public List<Fighter> GetQueue() {
            return _queue.ToList();
        }

        public void Start() {
            HasStarted = true;
            IsStopped = false;
            var gl1 = Exchange();
            var gl2 = Exchange();
            while (!UserAction) {
                Thread.Sleep(1000);
                Fight(gl1, gl2);
                if (gl1.HasDied) { 
                    HappeningManager.RaiseDeath(gl1);
                    gl1 = Exchange(); 
                }
                Thread.Sleep(1000);
                Fight(gl2, gl1);
                if (gl2.HasDied) { 
                    HappeningManager.RaiseDeath(gl2);
                    gl2 = Exchange();
                }
            }
        }

        private Fighter Exchange() {
            Fighter gl1;
            if (GladiatorRequest) {
                gl1 = _queue.Dequeue();
            }
            else {
                gl1 = FighterConverter.GetGladiator();
            }
            return gl1;
        }
        private static void Fight(Fighter g1, Fighter g2) {
            var att = g2.Attack();
            if (att > 0) {
                g1.Health -= att;
                HappeningManager.RaiseAttack(g1, g2, att);
            }
        }

    }
}
