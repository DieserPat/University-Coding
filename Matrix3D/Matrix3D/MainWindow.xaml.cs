using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace VectorMath
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Points and vectors
            Point3D p1 = new Point3D(1.5, 1, 0);
            double p1x = p1.X;
            Vector3D v1 = new Vector3D(-0.5, 0, 0);
            Vector3D v2 = new Vector3D(0, 1, 0);
            Vector3D v3 = v1 + 2 * v2;
            Point3D p2 = p1 + v3;
            double cosv1v2 = Vector3D.DotProduct(v1, v2);
            Vector3D normal = Vector3D.CrossProduct(v1, v2);
            Vector3D normalizedNormal = normal; normalizedNormal.Normalize();

            // Create (and append) transformations
            Matrix3D r = Matrix3D.Identity; r.Rotate(new Quaternion(new Vector3D(0, 1, 0), 90));
            Matrix3D s = Matrix3D.Identity; s.Scale(new Vector3D(2, 2, 2));
            Matrix3D t1 = Matrix3D.Identity; t1.Translate(new Vector3D(1, 2, 3));
            Matrix3D t2 = new Matrix3D(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                1, 2, 3, 1);//tx,ty,tz,1
            // here t1 == t2

            // Combine existing matrices
            Matrix3D m = r;
            m.Append(s);
            m.Append(t1);
            m.Invert();

            // Alternatively, concatenate transformations
            Matrix3D o = Matrix3D.Identity;
            o.Rotate(new Quaternion(new Vector3D(0, 1, 0), 90)); // in degrees, o.k.
            o.Scale(new Vector3D(2, 2, 2));
            o.Translate(new Vector3D(1, 2, 3));
            o.Invert();
            // here m == o

            // Transform points and vectors
            Vector3D v4 = m.Transform(v3);
            Point3D p3 = m.Transform(p2);

            // Create view matrix
            Vector3D lookDirection = new Vector3D(0, 0, -1);
            Vector3D upDirection = new Vector3D(0, 1, 0); //a
            Point3D p = new Point3D(0, 0, 0);
            Vector3D w = -lookDirection; w.Normalize();
            Vector3D u = Vector3D.CrossProduct(upDirection, w); u.Normalize();
            Vector3D v = Vector3D.CrossProduct(w, u);
            Matrix3D vInverse = new Matrix3D( //V^-1
                u.X, u.Y, u.Z, 0,
                v.X, v.Y, v.Z, 0,
                w.X, w.Y, w.Z, 0,
                p.X, p.Y, p.Z, 1);
            Matrix3D viewMatrix = vInverse; viewMatrix.Invert(); //V

            // Create perspective projection matrix
            double fieldOfView = 45;//degrees, WPF default
            double fieldOfViewRadians = 2 * Math.PI * fieldOfView / 360.0;
            double aspectRatio = 16.0 / 9.0; //W/H=viewport3D.ActualWidth/viewport3D.ActualHeight
            double n = 0.125;//nearPlaneDistance, WPF default
            double f = 1000000;//farPlaneDistance, infinite is WPF default
            double a = 1 / Math.Tan(fieldOfViewRadians / 2);//n/2w
            double b = aspectRatio * a;//w/h=(2n/h)/(2n/w)=W/H
            double c = f / (n - f);
            double d = n * f / (n - f);
            Matrix3D projectionMatrix = new Matrix3D(
                a, 0, 0, 0,
                0, b, 0, 0,
                0, 0, c, -1,
                0, 0, d, 0);
        }
    }
}
