namespace HTML_Crawler_Prototype
{
    public class MyLinkedList<T>
    {
        public class Node
        {
            //two way connected linked list
            //every node in the list points to the next and the previous node
            //used for the parent-child relation in the general tree created from the html file
            public Node Next; //points to the next element in the list
            public Node Prev; // points to the past element in the list
            public T Value; //contains the value of the node
        }

        private int _count = 0;
        private Node Head, Tail; //initialize the first and last element of the list

        public MyLinkedList()
        {
            Head = Tail = null; //set the first and last linked list element to be null by default
        }
        public int Count() => _count;
        public Node AddFirst(T value)
        {
            //Create a new node that points to the tail of the list
            Node newNode = new Node
            {
                Value = value,
                Next = Tail,
                Prev = null
            };
            //if head isnt null we make its prev node point to our new node
            //that we are inserting to be on the first place
            if (Head != null)
                Head.Prev = newNode;
            else
                Tail = newNode; //if head is null we set the tail of the list to become the new node

            Head = newNode; // set our new node to be the first in the list
            _count++;
            return newNode; // return the new node
        }

        public Node AddLast(T value)
        {
            Node newNode = new Node
            {
                Value = value,
                Next = null,
                Prev = Tail
            };
            if (Tail != null)
            {
                Tail.Next = newNode;
            }
            else
            {
                Tail = newNode;
            }

            Tail = newNode;

            _count++;
            return newNode;
        }

        public Node AddBefore(Node node, T value)
        {
            //protects from null reference exception if node is null
            if (node == null && Head != null)
                throw new ArgumentNullException("node");

            Node newNode = new Node //creates new node the next node of which is pointing to  node and
                                    //the next node of which is pointing to the next node of "node" of the node we insert it before
            {
                Value = value,
                Next = node, //the next node is the node we insert it before
                Prev = node?.Prev //points to the prev node of node if its null however the prev node of newNode is going to point at null
            };
            _count++;
            //if head is null then we are inserting the first element in the list
            if (Head == null)
            {
                Head = Tail = newNode;
                return newNode;
            }
            //if the node we are inserting after has a next node
            //that next node' s previous node is going to be pointing to the node we are currently inserting
            //
            //(node's prev node)    .....   (node)
            //                        ^
            //                        |
            //(newNode)----------------
            //                node's prev's next node     node s prev node
            //(node's prev node)  <------------> (newNode) <------------> (node)
            //
            if (node.Prev != null)
                node.Prev.Next = newNode;
            else
            {
                Head = newNode;
            }
            node.Prev = newNode;

            return newNode;
        }
        //returns the first element in the list
        public Node First() => Head;
        public Node Last() => Tail;

        public Node AddAfter(Node node, T value)
        {
            //protects from null reference exception if node is null
            if (node == null && Tail != null)
                throw new ArgumentNullException("node");

            Node newNode = new Node //creates new node the prev node of which is pointing to the prev node of the node we insert it before
            {
                Value = value,
                Next = node?.Next, //the next node is the node we insert it before
                Prev = node //points to the prev node of node if its null however the prev node of newNode is going to point at null
            };
            //if head is null then we are inserting the first element in the list
            _count++;
            if (Tail == null)
            {
                Head = Tail = newNode;
                return newNode;
            }
            //if the node we are inserting before has a previous node
            //that previous node' s next node is going to be pointing to the node we are currently inserting
            //
            //(node)    .....   (node's next node)
            //                        ^
            //                        |
            //(newNode)----------------
            //        node's next node      node s next's prev node
            //(node)  <------------> (newNode) <------------> (node's next node)
            //
            if (node.Next != null)
                node.Next.Prev = newNode;
            else
            {
                Tail = newNode;
            }
            Tail.Next = newNode;

            return newNode;
        }
        public void Remove(Node node)
        {
            _count--;
            if (node?.Next != null)
                node.Next.Prev = node.Prev;
            else
                Tail = node?.Prev;

            if (node?.Prev != null)
                node.Prev.Next = node.Next;

            else
                Head = node?.Next;
        }
    }
}