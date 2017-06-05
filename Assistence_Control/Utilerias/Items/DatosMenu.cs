using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistence_Control.Utilerias.Items
{
    public class DatosMenu
    {
        private string _imagen;
        private string _titulo;
        private string _elemento;
        private double? _orden;
        private string _descripcion;
        private int _tipo;
        private int _tileSize;
        private int _tileBadge;
        private string _backGroundColor;
        private string _foreGroundColor;
        private string _backGroundColorSlide;
        private string _parametros;

        public int TileSize
        {
            get { return _tileSize; }
            set { _tileSize = value; }
        }
        public int TileBadge
        {
            get { return _tileBadge; }
            set { _tileBadge = value; }
        }
        public string BackGroundColor
        {
            get { return _backGroundColor; }
            set { _backGroundColor = value; }
        }
        public string ForeGroundColor
        {
            get { return _foreGroundColor; }
            set { _foreGroundColor = value; }
        }
        public string BackGroundColorSlide
        {
            get { return _backGroundColorSlide; }
            set { _backGroundColorSlide = value; }
        }
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        public int Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }
        public string Imagen
        {
            get { return _imagen; }
            set { _imagen = value; }
        }
        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; }
        }
        public string Elemento
        {
            get { return _elemento; }
            set { _elemento = value; }
        }
        public double? Orden
        {
            get { return _orden; }
            set { _orden = value; }
        }
        public string Parametros
        {
            get { return _parametros; }
            set { _parametros = value; }
        }
    }

}
