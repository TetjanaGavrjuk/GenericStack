using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericStack
{
    public class CstmStack<T> : IEnumerable
        where T:struct
    {
        // начальный размер массива элементов
        private const long InitialLength = 3;

        private T[] Items;
        private int HeadIndx = -1;

        public int Count
        {
            get { return (HeadIndx + 1); }
        }

        #region constructors
        public CstmStack()
        {
            Items = new T[InitialLength];
        }

        public CstmStack(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            Items = new T[capacity];
          }
        #endregion


        #region Public Methods
        public void Push(T item)
        {
            // увеличить размер массива, если он заполнен более, чем на 80%
            if (FullnessPrc() > 80)
            {
                EnlargeArray();
            }
            HeadIndx++;
            Items[HeadIndx] = item;
        }

        public T Pop()
        {
            if (HeadIndx < 0)
            {
                throw new ArgumentOutOfRangeException(); 
            }
            //Возвращаем элемент из головы и передвигаем указатель головы
            return Items[HeadIndx--];
        }

        public T Peek()
        {
            if (HeadIndx < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return Items[HeadIndx];
        }

        public void ShowItems()
        {
            for (int i = HeadIndx; i >= 0; i--)
            {
                Console.Write($"{Items[i]}  ");
            }
            Console.WriteLine("\n");
        }
        #endregion

        #region Private Methods
        private double FullnessPrc()
        {
            return Math.Round((double)((HeadIndx < 1 ? 1 : HeadIndx + 1) / (Items.GetUpperBound(0) + 1) * 100), 0);
        }

        private void EnlargeArray()
        {
            //1.создать новый массив с размером вдвое большим, чем существующий
            T[] NewItems = new T[this.Items.Length*2];

            //2.скопировать в новый массив значения из старого
            for (int i = 0; i < this.Items.Length; i++)
            {
                NewItems[i] = this.Items[i];
            }

            //3.удаление старого массива возьмет на себя сборщик мусора
            // ??

            //4.ссылку на новый массив засунуть в ссылку на старый- просто переприсваисваем переменные
            //  при этом переменная Items будет указывать на область памяти с НОВЫМ массивом
            this.Items = NewItems;
        }
        #endregion

        #region IEnumerable 
        public IEnumerator GetEnumerator()
        {
            return new CustomStackEnumerator<T>(this);
        }
        #endregion

        #region IEnumerator 
        // В перечислителе наименование типа  T - совпадает с  наименованием T для  CstmStack<T>  ...
        private class CustomStackEnumerator<T> :  IEnumerator<T>
            where T:struct
        {
            private readonly CstmStack<T> Stack;
            private int CurrentIndex;

            public CustomStackEnumerator(CstmStack<T> stack)
            {
                this.Stack = stack;
                this.Reset();
            }
            public void Reset()
            {
                CurrentIndex = this.Stack.HeadIndx + 1;
            }

            public bool MoveNext()
            {
                //Если позиция елемента, лежит в пределах длины массива
                if ((CurrentIndex > 0) & (CurrentIndex <= this.Stack.HeadIndx + 1))
                {
                    CurrentIndex--;
                    return true;
                }
                else
                {
                    Reset();
                    return false;
                }
            }
            public T Current => this.Stack.Items[CurrentIndex];

            object IEnumerator.Current => this.Current;

            public void Dispose() { }
        }
        #endregion

    }
}
