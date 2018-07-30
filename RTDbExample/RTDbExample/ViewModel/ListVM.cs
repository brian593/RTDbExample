using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Streaming;
using RTDbExample.Database;
using RTDbExample.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using static RTDbExample.Helpers.BaseEnum;

namespace RTDbExample.ViewModel
{
    public class ListVM : BaseViewModel
    {
        private readonly string ENDERECO_FIREBASE = "https://elgasuio.firebaseio.com/";
        private readonly FirebaseClient _firebaseClient;

        private ObservableCollection<Student> _obs;

        public ObservableCollection<Student> obs
        {
            get { return _obs; }
            set { _obs = value; OnPropertyChanged(); }
        }

        public Student PedidoSelecionado;

        public ICommand AceitarPedidoCmd { get; set; }

        public ListVM()
        {
            _firebaseClient = new FirebaseClient(ENDERECO_FIREBASE);
            obs = new ObservableCollection<Student>();
            //AceitarPedidoCmd = new Command(() => AceitarPedido());
            ListenerPedidos();
        }

        //private void AceitarPedido()
        //{
        //    PedidoSelecionado.IdVendedor = 1;
        //    _firebaseClient
        //        .Child("pedidos")
        //        .Child(PedidoSelecionado.KeyPedido)
        //        .PutAsync(PedidoSelecionado);
        //}

        private void ListenerPedidos()
        {
            _firebaseClient
                .Child("Student")
                .AsObservable<Student>()
                .Subscribe(d =>
                {
                    if (d.EventType == FirebaseEventType.InsertOrUpdate)
                    {
                        AdicionarPedido(d.Key, d.Object);

                    }
                    if (d.EventType == FirebaseEventType.Delete)
                    {
                        //accion para borrar
                    }

                }); 

        }

        private void AdicionarPedido(string key, Student pedido)
        {
            obs.Add(new Student()
            {
                age = pedido.age,
                name = pedido.name,
            });
        }

      
    }
}
