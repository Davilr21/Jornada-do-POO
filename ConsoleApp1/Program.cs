using System;
using System.Collections.Generic;

namespace JornadaPooJoao
{
    // --------------------------------------------------
    // ABSTRAÇÃO: Esconde detalhes técnicos do modelo 
    // e mostra só o que importa para o cliente.
    // --------------------------------------------------
    public abstract class VeiculoTerrestre
    {
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public decimal ConsumoCombustivel { get; set; }

        public VeiculoTerrestre(string modelo, string marca, decimal consumoCombustivel)
        {
            Modelo = modelo;
            Marca = marca;
            ConsumoCombustivel = consumoCombustivel;
        }

        // Método obrigatório para todos os veículos
        public abstract void Mover();

        // Apresentação simplificada das qualidades do veículo
        public virtual void ExibirDestaquesComerciais()
        {
            Console.WriteLine($"Veículo: {Modelo} ({Marca}) | Consumo: {ConsumoCombustivel} km/l");
        }
    }

    // --------------------------------------------------
    // CLASSE E ENCAPSULAMENTO:
    // Classe = O projeto/planta do carro.
    // Encapsulamento = Protege partes internas (motor, injeção)
    // e libera apenas comandos simples (botão de ligar).
    // --------------------------------------------------
    public class CarroJoao : VeiculoTerrestre
    {
        // Segredos internos (ninguém de fora mexe)
        private bool _motorLigado;
        private int _pressaoInjecaoEletronica;
        protected int CapacidadeCargaKg;

        // Propriedades visíveis
        public int Rodas { get; private set; }
        public int Assentos { get; private set; }

        public CarroJoao(string modelo, decimal consumo, int assentos = 5)
            : base(modelo, "CarroJoão Inc.", consumo)
        {
            Rodas = 4;
            Assentos = assentos;
            _motorLigado = false;
            _pressaoInjecaoEletronica = 0;
            CapacidadeCargaKg = 400;
        }

        // Botão de partida simples
        public void LigarMotor()
        {
            AjustarInjecaoEletronica(); // Trabalho interno automático
            _motorLigado = true;
            Console.WriteLine($"[{Modelo}] Motor ligado!");
        }

        // Detalhe técnico escondido
        private void AjustarInjecaoEletronica()
        {
            _pressaoInjecaoEletronica = 250;
        }

        public override void Mover()
        {
            if (_motorLigado)
            {
                Console.WriteLine($"[{Modelo}] O carro está rodando na rua.");
            }
            else
            {
                Console.WriteLine($"[{Modelo}] Ligue o motor primeiro!");
            }
        }

        public override void ExibirDestaquesComerciais()
        {
            base.ExibirDestaquesComerciais();
            Console.WriteLine($" -> Destaque: Muito espaçoso, leva {Assentos} pessoas!");
        }
    }

    // --------------------------------------------------
    // HERANÇA: Novos modelos reutilizam e modificam 
    // a estrutura do CarroJoão original.
    // --------------------------------------------------

    // Modelo Esportivo
    public class CarroJoaoSports : CarroJoao
    {
        public int PotenciaCavalos { get; set; }

        public CarroJoaoSports(string modelo, decimal consumo, int potencia)
            : base(modelo, consumo, assentos: 2)
        {
            PotenciaCavalos = potencia;
        }

        public override void Mover()
        {
            Console.WriteLine($"[{Modelo}] O carro esportivo acelera muito rápido!");
        }
    }

    // Modelo SUV
    public class CarroJoaoSUV : CarroJoao
    {
        public CarroJoaoSUV(string modelo, decimal consumo, int capacidadeCarga)
            : base(modelo, consumo, assentos: 7)
        {
            CapacidadeCargaKg = capacidadeCarga;
        }

        public override void Mover()
        {
            Console.WriteLine($"[{Modelo}] O SUV anda firme em estradas de terra.");
        }
    }

    // Modelo Elétrico
    public class CarroJoaoEco : CarroJoao
    {
        public int AutonomiaBateriaKm { get; set; }

        public CarroJoaoEco(string modelo, int autonomia)
            : base(modelo, consumo: 0)
        {
            AutonomiaBateriaKm = autonomia;
        }

        public override void Mover()
        {
            Console.WriteLine($"[{Modelo}] O carro elétrico anda sem fazer barulho.");
        }
    }

    // --------------------------------------------------
    // POLIMORFISMO: Diferentes veículos usam a mesma ação 
    // Mover(), mas cada um se comporta do seu jeito.
    // --------------------------------------------------

    public class OnibusJoao : VeiculoTerrestre
    {
        public int CapacidadePassageiros { get; set; }

        public OnibusJoao(string modelo, int passageiros)
            : base(modelo, "ÔnibusJoão Inc.", 3.5m)
        {
            CapacidadePassageiros = passageiros;
        }

        public override void Mover()
        {
            Console.WriteLine($"[{Modelo}] O ônibus anda parando nos pontos para os passageiros.");
        }
    }

    public class CaminhaoJoao : VeiculoTerrestre
    {
        public double CapacidadeToneladas { get; set; }

        public CaminhaoJoao(string modelo, double toneladas)
            : base(modelo, "CaminhãoJoão Inc.", 2.1m)
        {
            CapacidadeToneladas = toneladas;
        }

        public override void Mover()
        {
            Console.WriteLine($"[{Modelo}] O caminhão se move devagar levando muita carga.");
        }
    }

    public class MotoJoao : VeiculoTerrestre
    {
        public MotoJoao(string modelo)
            : base(modelo, "MotoJoão Inc.", 35.0m) { }

        public override void Mover()
        {
            Console.WriteLine($"[{Modelo}] A moto anda rápido desviando do trânsito.");
        }
    }

    // --------------------------------------------------
    // PROGRAMA PRINCIPAL
    // --------------------------------------------------
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- 1. Criando um Objeto a partir da Classe ---");
            // Criando o carro físico (Objeto) com base no projeto (Classe)
            CarroJoao meuCarro = new CarroJoao("CarroJoão Básico", 14.5m);
            meuCarro.LigarMotor();
            meuCarro.Mover();
            Console.WriteLine();

            Console.WriteLine("--- 2. Herança (Variantes do Carro) ---");
            CarroJoaoSports esportivo = new CarroJoaoSports("CarroJoão Turbo", 8.2m, 450);
            CarroJoaoEco eletrico = new CarroJoaoEco("CarroJoão Eco", 400);

            esportivo.LigarMotor();
            esportivo.Mover();
            eletrico.Mover();
            Console.WriteLine();

            Console.WriteLine("--- 3. Abstração (Foco no cliente) ---");
            meuCarro.ExibirDestaquesComerciais();

            Console.WriteLine("--- 4. Polimorfismo (Diferentes veículos se movendo) ---");
            // Uma lista de veículos, onde cada um se move do seu jeito
            List<VeiculoTerrestre> frota = new List<VeiculoTerrestre>
            {
                meuCarro,
                esportivo,
                eletrico,
                new OnibusJoao("Ônibus da Cidade", 50),
                new CaminhaoJoao("Caminhão Carga", 20.0),
                new MotoJoao("Moto 250cc")
            };

            foreach (VeiculoTerrestre v in frota)
            {
                v.Mover();
            }
        }
    }
}