using System.Collections;
using System.Collections.Generic;

namespace Tyd
{
    ///<summary>
    /// A TydNode that contains a collection of sub-nodes.
    ///</summary>
    public abstract class TydCollection : TydNode, IEnumerable<TydNode>
    {
        //Data
        protected List<TydNode> nodes = new List<TydNode>();
        protected string attHandle;
        protected string attSource;
        protected bool attAbstract;
        protected List<KeyValuePair<string,string>> attAdditional;

        //Properties
        public int Count
        {
            get{return nodes.Count;}
        }

        public List<TydNode> Nodes
        {
            get{return nodes;}
        }

        public string AttributeHandle
        {
            get{return attHandle;}
            set{attHandle = value;}
        }

        public string AttributeSource
        {
            get{return attSource;}
            set{attSource = value;}
        }

        public bool AttributeAbstract
        {
            get{return attAbstract;}
            set{attAbstract = value;}
        }

        public List<KeyValuePair<string,string>> AdditionalAttributes
        {
            get { return attAdditional; }
        }

        public TydNode this[int index]
        {
            get
            {
                return nodes[index];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TydNode> GetEnumerator()
        {
            foreach (TydNode n in nodes)
            {
                yield return n;
            }
        }

        public TydCollection(string name, TydNode parent, int docLine = -1) : base(name, parent, docLine)
        {
            attAdditional = new List<KeyValuePair<string, string>>();
        }

        public void SetupAttributes(string attHandle, string attSource, bool attAbstract)
        {
            this.attHandle = attHandle;
            this.attSource = attSource;
            this.attAbstract = attAbstract;
        }

        public void SetupAttributes(string attHandle, string attSource, bool attAbstract, List<KeyValuePair<string,string>> attAdditional)
        {
            this.attHandle = attHandle;
            this.attSource = attSource;
            this.attAbstract = attAbstract;
            this.attAdditional = attAdditional;
        }

        ///<summary>
        /// Add a node as a child of this node, and link it as a parent.
        ///</summary>
        public void AddChild(TydNode node)
        {
            nodes.Add(node);
            node.Parent = this;
        }

        public string GetAdditionalAttributeValue(string attribute)
        {
            if(attAdditional != null
                && attAdditional.Count > 0)
            {
                foreach(KeyValuePair<string,string> pair in attAdditional)
                {
                    if(pair.Key == attribute)
                    {
                        return pair.Value;
                    }
                }
            }

            return null;
        }

        public void InsertChild(TydNode node, int index)
        {
            nodes.Insert(index, node);
            node.Parent = this;
        }

        protected void CopyDataFrom(TydCollection other)
        {
            other.docIndexEnd = docIndexEnd;
            other.attHandle = attHandle;
            other.attSource = attSource;
            other.attAbstract = attAbstract;
            for (int i = 0; i < nodes.Count; i++)
            {
                other.AddChild(nodes[i].DeepClone());
            }

            // Copy additional attribute
            if(attAdditional != null)
            {
                other.attAdditional = new List<KeyValuePair<string,string>>(attAdditional);
            }
        }
    }

}