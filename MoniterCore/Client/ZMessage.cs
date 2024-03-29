﻿////
////  Multipart message class for example applications.

////  Author:     Michael Compton
////  Email:      michael.compton@littleedge.co.uk

//using System.Collections.Generic;
//using System.Text;
//using ZeroMQ;

//namespace ZeroMQ
//{
//    public class ZMessage
//    {
//        readonly Encoding encoding = Encoding.UTF8;
//        readonly List<byte[]> frames = new List<byte[]>();

//        public ZMessage()
//        {

//        }

//        public ZMessage(ZeroMQ.ZmqSocket socket)
//        {
//            Receive(socket);
//        }

//        public ZMessage(string body, Encoding encoding)
//        {
//            frames.Add(encoding.GetBytes(body));
//            this.encoding = encoding;
//        }

//        public ZMessage(string body)
//        {
//            frames.Add(encoding.GetBytes(body));
//        }

//        public ZMessage(byte[] body)
//        {
//            frames.Add(body);
//        }
//        public void Receive(ZmqSocket ZmqSocket)
//        {
//            var zmqMessage = ZmqSocket.ReceiveMessage();

//            foreach (var frame in zmqMessage)
//            {
//                frames.Insert(0, frame.Buffer);
//            }
//        }

//        public void Send(ZmqSocket socket)
//        {
//            for (int index = frames.Count - 1; index > 0; index--)
//            {
//                socket.SendMore(frames[index]);
//            }

//            socket.Send(frames[0]);
//        }


//        public string BodyToString()
//        {
//            return encoding.GetString(Body);
//        }

//        public void StringToBody(string body)
//        {
//            Body = encoding.GetBytes(body);
//        }

//        public void Append(byte[] data)
//        {
//            frames.Insert(0, data);
//        }

//        public byte[] Pop()
//        {
//            byte[] data = frames[frames.Count - 1];
//            frames.RemoveAt(frames.Count - 1);
//            return data;
//        }

//        public void Push(byte[] data)
//        {
//            frames.Add(data);
//        }

//        public void Wrap(byte[] address, byte[] delim)
//        {
//            if (delim != null)
//            {
//                frames.Add(delim);
//            }
//            frames.Add(address);
//        }

//        public byte[] Unwrap()
//        {
//            byte[] addr = Pop();
//            if (Address.Length == 0)
//            {
//                Pop();
//            }
//            return addr;
//        }

//        public int FrameCount
//        {
//            get { return frames.Count; }
//        }

//        public byte[] Address
//        {
//            get { return frames[frames.Count - 1]; }
//            set { frames.Add(value); }
//        }

//        //改变消息源地址
//        public void AddressFromString(string address)
//        {
//            Unwrap();
//            Wrap(encoding.GetBytes(address), encoding.GetBytes(""));

//        }
//        public string AddressToString()
//        {
//            return encoding.GetString(Address);
//        }
//        public byte[] Body
//        {
//            get { return frames[0]; }
//            set { frames[0] = value; }
//        }
//    }
//}

