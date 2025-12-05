namespace executarSQLEmLote
{
    partial class TelaPrincipal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            label2 = new Label();
            txtIpServidor = new TextBox();
            label1 = new Label();
            cboxBaseDados = new ComboBox();
            btnExecutar = new Button();
            btnListarBases = new Button();
            groupBox2 = new GroupBox();
            gridListaTabelas = new DataGridView();
            nomeTabela = new DataGridViewTextBoxColumn();
            groupBox3 = new GroupBox();
            txtLog = new TextBox();
            btnProcedures = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridListaTabelas).BeginInit();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtIpServidor);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(cboxBaseDados);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(405, 56);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Base Homologação";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(181, 25);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 3;
            label2.Text = "Base";
            // 
            // txtIpServidor
            // 
            txtIpServidor.Location = new Point(62, 22);
            txtIpServidor.Name = "txtIpServidor";
            txtIpServidor.Size = new Size(113, 23);
            txtIpServidor.TabIndex = 0;
            txtIpServidor.Text = "localhost";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 25);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 1;
            label1.Text = "Servidor";
            // 
            // cboxBaseDados
            // 
            cboxBaseDados.FormattingEnabled = true;
            cboxBaseDados.Location = new Point(218, 22);
            cboxBaseDados.Name = "cboxBaseDados";
            cboxBaseDados.Size = new Size(178, 23);
            cboxBaseDados.TabIndex = 1;
            // 
            // btnExecutar
            // 
            btnExecutar.Location = new Point(585, 12);
            btnExecutar.Name = "btnExecutar";
            btnExecutar.Size = new Size(75, 56);
            btnExecutar.TabIndex = 3;
            btnExecutar.Text = "Executar";
            btnExecutar.UseVisualStyleBackColor = true;
            btnExecutar.Click += btnExecutar_Click;
            // 
            // btnListarBases
            // 
            btnListarBases.Location = new Point(423, 12);
            btnListarBases.Name = "btnListarBases";
            btnListarBases.Size = new Size(75, 56);
            btnListarBases.TabIndex = 2;
            btnListarBases.Text = "Listar Bases";
            btnListarBases.UseVisualStyleBackColor = true;
            btnListarBases.Click += btnListarBases_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox2.Controls.Add(gridListaTabelas);
            groupBox2.Location = new Point(12, 74);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(175, 364);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Tabelas a Atualizar";
            // 
            // gridListaTabelas
            // 
            gridListaTabelas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridListaTabelas.Columns.AddRange(new DataGridViewColumn[] { nomeTabela });
            gridListaTabelas.Dock = DockStyle.Fill;
            gridListaTabelas.Location = new Point(3, 19);
            gridListaTabelas.Name = "gridListaTabelas";
            gridListaTabelas.Size = new Size(169, 342);
            gridListaTabelas.TabIndex = 0;
            // 
            // nomeTabela
            // 
            nomeTabela.HeaderText = "Tabela";
            nomeTabela.Name = "nomeTabela";
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox3.Controls.Add(txtLog);
            groupBox3.Location = new Point(193, 74);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(305, 364);
            groupBox3.TabIndex = 5;
            groupBox3.TabStop = false;
            groupBox3.Text = "Log de Execução";
            // 
            // txtLog
            // 
            txtLog.Dock = DockStyle.Fill;
            txtLog.Location = new Point(3, 19);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Both;
            txtLog.Size = new Size(299, 342);
            txtLog.TabIndex = 0;
            // 
            // btnProcedures
            // 
            btnProcedures.Location = new Point(504, 12);
            btnProcedures.Name = "btnProcedures";
            btnProcedures.Size = new Size(75, 56);
            btnProcedures.TabIndex = 6;
            btnProcedures.Text = "Criar Procedures";
            btnProcedures.UseVisualStyleBackColor = true;
            btnProcedures.Click += this.btnProcedures_Click;
            // 
            // TelaPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnProcedures);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(btnListarBases);
            Controls.Add(btnExecutar);
            Controls.Add(groupBox1);
            Name = "TelaPrincipal";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridListaTabelas).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox txtIpServidor;
        private Label label1;
        private ComboBox cboxBaseDados;
        private Label label2;
        private Button btnExecutar;
        private Button btnListarBases;
        private GroupBox groupBox2;
        private DataGridView gridListaTabelas;
        private DataGridViewTextBoxColumn nomeTabela;
        private GroupBox groupBox3;
        private TextBox txtLog;
        private Button btnProcedures;
    }
}
