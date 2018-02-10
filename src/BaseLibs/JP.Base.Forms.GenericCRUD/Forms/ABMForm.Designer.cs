using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace JP.Base.Forms.GenericCRUD.Forms
{

	partial class ABMForm
	{

		//Form reemplaza a Dispose para limpiar la lista de componentes.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		//Requerido por el Diseñador de Windows Forms

		private System.ComponentModel.IContainer components;
		//NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
		//Se puede modificar usando el Diseñador de Windows Forms.  
		//No lo modifique con el editor de código.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ABMForm));
            this.GrpDatosCargados = new System.Windows.Forms.GroupBox();
            this.SPCDatosCargados = new System.Windows.Forms.SplitContainer();
            this.PanelBar = new System.Windows.Forms.Panel();
            this.tblToolbar = new System.Windows.Forms.TableLayoutPanel();
            this.PBar = new System.Windows.Forms.ProgressBar();
            this.TViewCategorias = new System.Windows.Forms.TreeView();
            this.GrpCategorias = new System.Windows.Forms.GroupBox();
            this.GrpCargaDatos = new System.Windows.Forms.GroupBox();
            this.PNLCategorias = new System.Windows.Forms.Panel();
            this.PNLDatosCargados = new System.Windows.Forms.Panel();
            this.PNLCargarDatos = new System.Windows.Forms.Panel();
            this.PNLToolBar = new System.Windows.Forms.Panel();
            this.PNLContainer = new System.Windows.Forms.Panel();
            this.PixBox = new System.Windows.Forms.PictureBox();
            this.CMDBuscar = new System.Windows.Forms.Button();
            this.CMDConsultar = new System.Windows.Forms.Button();
            this.CMDNuevo = new System.Windows.Forms.Button();
            this.CMDEliminar = new System.Windows.Forms.Button();
            this.CMDEditar = new System.Windows.Forms.Button();
            this.CMDCancelar = new System.Windows.Forms.Button();
            this.VerticalSplitter = new NJFLib.Controls.CollapsibleSplitter();
            this.HorizontalSplitter = new NJFLib.Controls.CollapsibleSplitter();
            this.GrpDatosCargados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SPCDatosCargados)).BeginInit();
            this.SPCDatosCargados.Panel1.SuspendLayout();
            this.SPCDatosCargados.SuspendLayout();
            this.PanelBar.SuspendLayout();
            this.tblToolbar.SuspendLayout();
            this.GrpCategorias.SuspendLayout();
            this.PNLCategorias.SuspendLayout();
            this.PNLDatosCargados.SuspendLayout();
            this.PNLCargarDatos.SuspendLayout();
            this.PNLToolBar.SuspendLayout();
            this.PNLContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PixBox)).BeginInit();
            this.SuspendLayout();
            // 
            // GrpDatosCargados
            // 
            this.GrpDatosCargados.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GrpDatosCargados.Controls.Add(this.SPCDatosCargados);
            this.GrpDatosCargados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrpDatosCargados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrpDatosCargados.Location = new System.Drawing.Point(5, 5);
            this.GrpDatosCargados.Margin = new System.Windows.Forms.Padding(5);
            this.GrpDatosCargados.Name = "GrpDatosCargados";
            this.GrpDatosCargados.Padding = new System.Windows.Forms.Padding(5);
            this.GrpDatosCargados.Size = new System.Drawing.Size(679, 221);
            this.GrpDatosCargados.TabIndex = 2;
            this.GrpDatosCargados.TabStop = false;
            this.GrpDatosCargados.Text = "Datos cargados";
            // 
            // SPCDatosCargados
            // 
            this.SPCDatosCargados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SPCDatosCargados.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SPCDatosCargados.IsSplitterFixed = true;
            this.SPCDatosCargados.Location = new System.Drawing.Point(5, 19);
            this.SPCDatosCargados.Name = "SPCDatosCargados";
            // 
            // SPCDatosCargados.Panel1
            // 
            this.SPCDatosCargados.Panel1.Controls.Add(this.PixBox);
            this.SPCDatosCargados.Panel1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // SPCDatosCargados.Panel2
            // 
            this.SPCDatosCargados.Panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SPCDatosCargados.Panel2.Padding = new System.Windows.Forms.Padding(5);
            this.SPCDatosCargados.Size = new System.Drawing.Size(669, 197);
            this.SPCDatosCargados.SplitterDistance = 56;
            this.SPCDatosCargados.TabIndex = 24;
            // 
            // PanelBar
            // 
            this.PanelBar.AutoSize = true;
            this.PanelBar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PanelBar.Controls.Add(this.tblToolbar);
            this.PanelBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelBar.Location = new System.Drawing.Point(5, 5);
            this.PanelBar.Name = "PanelBar";
            this.PanelBar.Padding = new System.Windows.Forms.Padding(3);
            this.PanelBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PanelBar.Size = new System.Drawing.Size(856, 46);
            this.PanelBar.TabIndex = 0;
            // 
            // tblToolbar
            // 
            this.tblToolbar.AutoSize = true;
            this.tblToolbar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblToolbar.ColumnCount = 7;
            this.tblToolbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblToolbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblToolbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblToolbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblToolbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblToolbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblToolbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblToolbar.Controls.Add(this.CMDBuscar, 5, 0);
            this.tblToolbar.Controls.Add(this.PBar, 6, 0);
            this.tblToolbar.Controls.Add(this.CMDConsultar, 1, 0);
            this.tblToolbar.Controls.Add(this.CMDNuevo, 0, 0);
            this.tblToolbar.Controls.Add(this.CMDEliminar, 4, 0);
            this.tblToolbar.Controls.Add(this.CMDEditar, 2, 0);
            this.tblToolbar.Controls.Add(this.CMDCancelar, 3, 0);
            this.tblToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblToolbar.Location = new System.Drawing.Point(3, 3);
            this.tblToolbar.Name = "tblToolbar";
            this.tblToolbar.RowCount = 1;
            this.tblToolbar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblToolbar.Size = new System.Drawing.Size(850, 40);
            this.tblToolbar.TabIndex = 0;
            // 
            // PBar
            // 
            this.PBar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.PBar.Location = new System.Drawing.Point(738, 5);
            this.PBar.Name = "PBar";
            this.PBar.Size = new System.Drawing.Size(109, 30);
            this.PBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.PBar.TabIndex = 6;
            // 
            // TViewCategorias
            // 
            this.TViewCategorias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TViewCategorias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TViewCategorias.HideSelection = false;
            this.TViewCategorias.Indent = 19;
            this.TViewCategorias.ItemHeight = 16;
            this.TViewCategorias.Location = new System.Drawing.Point(10, 19);
            this.TViewCategorias.Margin = new System.Windows.Forms.Padding(5);
            this.TViewCategorias.Name = "TViewCategorias";
            this.TViewCategorias.Size = new System.Drawing.Size(139, 442);
            this.TViewCategorias.TabIndex = 0;
            this.TViewCategorias.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TViewCategorias_AfterSelect);
            this.TViewCategorias.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TViewCategorias_NodeMouseDoubleClick);
            // 
            // GrpCategorias
            // 
            this.GrpCategorias.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GrpCategorias.Controls.Add(this.TViewCategorias);
            this.GrpCategorias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrpCategorias.Location = new System.Drawing.Point(5, 5);
            this.GrpCategorias.Margin = new System.Windows.Forms.Padding(5);
            this.GrpCategorias.Name = "GrpCategorias";
            this.GrpCategorias.Padding = new System.Windows.Forms.Padding(10, 5, 10, 10);
            this.GrpCategorias.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GrpCategorias.Size = new System.Drawing.Size(159, 471);
            this.GrpCategorias.TabIndex = 1;
            this.GrpCategorias.TabStop = false;
            this.GrpCategorias.Text = "Categorías";
            // 
            // GrpCargaDatos
            // 
            this.GrpCargaDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrpCargaDatos.Location = new System.Drawing.Point(5, 5);
            this.GrpCargaDatos.Name = "GrpCargaDatos";
            this.GrpCargaDatos.Padding = new System.Windows.Forms.Padding(5);
            this.GrpCargaDatos.Size = new System.Drawing.Size(679, 232);
            this.GrpCargaDatos.TabIndex = 3;
            this.GrpCargaDatos.TabStop = false;
            this.GrpCargaDatos.Text = "Cargar datos";
            // 
            // PNLCategorias
            // 
            this.PNLCategorias.Controls.Add(this.GrpCategorias);
            this.PNLCategorias.Dock = System.Windows.Forms.DockStyle.Left;
            this.PNLCategorias.Location = new System.Drawing.Point(5, 61);
            this.PNLCategorias.Name = "PNLCategorias";
            this.PNLCategorias.Padding = new System.Windows.Forms.Padding(5);
            this.PNLCategorias.Size = new System.Drawing.Size(169, 481);
            this.PNLCategorias.TabIndex = 5;
            // 
            // PNLDatosCargados
            // 
            this.PNLDatosCargados.Controls.Add(this.GrpDatosCargados);
            this.PNLDatosCargados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PNLDatosCargados.Location = new System.Drawing.Point(0, 0);
            this.PNLDatosCargados.Name = "PNLDatosCargados";
            this.PNLDatosCargados.Padding = new System.Windows.Forms.Padding(5);
            this.PNLDatosCargados.Size = new System.Drawing.Size(689, 231);
            this.PNLDatosCargados.TabIndex = 6;
            // 
            // PNLCargarDatos
            // 
            this.PNLCargarDatos.Controls.Add(this.GrpCargaDatos);
            this.PNLCargarDatos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PNLCargarDatos.Location = new System.Drawing.Point(0, 239);
            this.PNLCargarDatos.Name = "PNLCargarDatos";
            this.PNLCargarDatos.Padding = new System.Windows.Forms.Padding(5);
            this.PNLCargarDatos.Size = new System.Drawing.Size(689, 242);
            this.PNLCargarDatos.TabIndex = 7;
            // 
            // PNLToolBar
            // 
            this.PNLToolBar.AutoSize = true;
            this.PNLToolBar.Controls.Add(this.PanelBar);
            this.PNLToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.PNLToolBar.Location = new System.Drawing.Point(5, 5);
            this.PNLToolBar.Name = "PNLToolBar";
            this.PNLToolBar.Padding = new System.Windows.Forms.Padding(5);
            this.PNLToolBar.Size = new System.Drawing.Size(866, 56);
            this.PNLToolBar.TabIndex = 7;
            // 
            // PNLContainer
            // 
            this.PNLContainer.Controls.Add(this.PNLDatosCargados);
            this.PNLContainer.Controls.Add(this.VerticalSplitter);
            this.PNLContainer.Controls.Add(this.PNLCargarDatos);
            this.PNLContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PNLContainer.Location = new System.Drawing.Point(182, 61);
            this.PNLContainer.Name = "PNLContainer";
            this.PNLContainer.Size = new System.Drawing.Size(689, 481);
            this.PNLContainer.TabIndex = 10;
            // 
            // PixBox
            // 
            this.PixBox.BackgroundImage = global::JP.Base.Forms.GenericCRUD.Properties.Resources.font_awesome_4_7_0_list_256_0_000000_none;
            this.PixBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PixBox.Location = new System.Drawing.Point(5, 0);
            this.PixBox.Margin = new System.Windows.Forms.Padding(0);
            this.PixBox.Name = "PixBox";
            this.PixBox.Size = new System.Drawing.Size(48, 48);
            this.PixBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PixBox.TabIndex = 23;
            this.PixBox.TabStop = false;
            // 
            // CMDBuscar
            // 
            this.CMDBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.CMDBuscar.Image = global::JP.Base.Forms.GenericCRUD.Properties.Resources.font_awesome_4_7_0_search_16_0_000000_none;
            this.CMDBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CMDBuscar.Location = new System.Drawing.Point(535, 5);
            this.CMDBuscar.Margin = new System.Windows.Forms.Padding(5);
            this.CMDBuscar.Name = "CMDBuscar";
            this.CMDBuscar.Size = new System.Drawing.Size(96, 30);
            this.CMDBuscar.TabIndex = 5;
            this.CMDBuscar.Text = "&Buscar";
            this.CMDBuscar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.CMDBuscar.UseVisualStyleBackColor = true;
            this.CMDBuscar.Click += new System.EventHandler(this.CMDBuscar_Click);
            // 
            // CMDConsultar
            // 
            this.CMDConsultar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.CMDConsultar.Image = global::JP.Base.Forms.GenericCRUD.Properties.Resources.foundation_icon_fonts_2015_02_16_page_search_16_0_000000_none;
            this.CMDConsultar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CMDConsultar.Location = new System.Drawing.Point(111, 5);
            this.CMDConsultar.Margin = new System.Windows.Forms.Padding(5);
            this.CMDConsultar.Name = "CMDConsultar";
            this.CMDConsultar.Size = new System.Drawing.Size(96, 30);
            this.CMDConsultar.TabIndex = 7;
            this.CMDConsultar.Text = "Cons&ultar";
            this.CMDConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.CMDConsultar.UseVisualStyleBackColor = true;
            this.CMDConsultar.Click += new System.EventHandler(this.CMDConsultar_Click);
            // 
            // CMDNuevo
            // 
            this.CMDNuevo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.CMDNuevo.Image = global::JP.Base.Forms.GenericCRUD.Properties.Resources.font_awesome_4_7_0_plus_16_0_000000_none1;
            this.CMDNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CMDNuevo.Location = new System.Drawing.Point(5, 5);
            this.CMDNuevo.Margin = new System.Windows.Forms.Padding(5);
            this.CMDNuevo.Name = "CMDNuevo";
            this.CMDNuevo.Size = new System.Drawing.Size(96, 30);
            this.CMDNuevo.TabIndex = 1;
            this.CMDNuevo.Text = "&Nuevo";
            this.CMDNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.CMDNuevo.UseVisualStyleBackColor = true;
            this.CMDNuevo.Click += new System.EventHandler(this.CMDNuevo_Click);
            // 
            // CMDEliminar
            // 
            this.CMDEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.CMDEliminar.Image = global::JP.Base.Forms.GenericCRUD.Properties.Resources.icomoon_free_2014_12_23_cross_16_0_000000_none;
            this.CMDEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CMDEliminar.Location = new System.Drawing.Point(429, 5);
            this.CMDEliminar.Margin = new System.Windows.Forms.Padding(5);
            this.CMDEliminar.Name = "CMDEliminar";
            this.CMDEliminar.Size = new System.Drawing.Size(96, 30);
            this.CMDEliminar.TabIndex = 4;
            this.CMDEliminar.Text = "E&liminar";
            this.CMDEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.CMDEliminar.UseVisualStyleBackColor = true;
            this.CMDEliminar.Click += new System.EventHandler(this.CMDEliminar_Click);
            // 
            // CMDEditar
            // 
            this.CMDEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.CMDEditar.Image = global::JP.Base.Forms.GenericCRUD.Properties.Resources.material_icons_3_0_1_mode_edit_16_0_000000_none;
            this.CMDEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CMDEditar.Location = new System.Drawing.Point(217, 5);
            this.CMDEditar.Margin = new System.Windows.Forms.Padding(5);
            this.CMDEditar.Name = "CMDEditar";
            this.CMDEditar.Size = new System.Drawing.Size(96, 30);
            this.CMDEditar.TabIndex = 2;
            this.CMDEditar.Text = "&Editar";
            this.CMDEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.CMDEditar.UseVisualStyleBackColor = true;
            this.CMDEditar.Click += new System.EventHandler(this.CMDEditar_Click);
            // 
            // CMDCancelar
            // 
            this.CMDCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.CMDCancelar.Image = global::JP.Base.Forms.GenericCRUD.Properties.Resources.typicons_2_0_7_cancel_outline_16_0_000000_none;
            this.CMDCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CMDCancelar.Location = new System.Drawing.Point(323, 5);
            this.CMDCancelar.Margin = new System.Windows.Forms.Padding(5);
            this.CMDCancelar.Name = "CMDCancelar";
            this.CMDCancelar.Size = new System.Drawing.Size(96, 30);
            this.CMDCancelar.TabIndex = 3;
            this.CMDCancelar.Text = "&Cancelar";
            this.CMDCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.CMDCancelar.UseVisualStyleBackColor = true;
            this.CMDCancelar.Click += new System.EventHandler(this.CMDCancelar_Click);
            // 
            // VerticalSplitter
            // 
            this.VerticalSplitter.AnimationDelay = 20;
            this.VerticalSplitter.AnimationStep = 20;
            this.VerticalSplitter.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
            this.VerticalSplitter.ControlToHide = this.PNLCargarDatos;
            this.VerticalSplitter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.VerticalSplitter.ExpandParentForm = false;
            this.VerticalSplitter.Location = new System.Drawing.Point(0, 231);
            this.VerticalSplitter.Name = "CollapsibleSplitter2";
            this.VerticalSplitter.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.VerticalSplitter.TabIndex = 10;
            this.VerticalSplitter.TabStop = false;
            this.VerticalSplitter.UseAnimations = true;
            this.VerticalSplitter.VisualStyle = NJFLib.Controls.VisualStyles.DoubleDots;
            // 
            // HorizontalSplitter
            // 
            this.HorizontalSplitter.AnimationDelay = 20;
            this.HorizontalSplitter.AnimationStep = 20;
            this.HorizontalSplitter.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
            this.HorizontalSplitter.ControlToHide = this.PNLCategorias;
            this.HorizontalSplitter.ExpandParentForm = false;
            this.HorizontalSplitter.Location = new System.Drawing.Point(174, 61);
            this.HorizontalSplitter.Name = "CollapsibleSplitter1";
            this.HorizontalSplitter.TabIndex = 8;
            this.HorizontalSplitter.TabStop = false;
            this.HorizontalSplitter.UseAnimations = true;
            this.HorizontalSplitter.VisualStyle = NJFLib.Controls.VisualStyles.DoubleDots;
            // 
            // ABMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 550);
            this.Controls.Add(this.PNLContainer);
            this.Controls.Add(this.HorizontalSplitter);
            this.Controls.Add(this.PNLCategorias);
            this.Controls.Add(this.PNLToolBar);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ABMForm";
            this.Padding = new System.Windows.Forms.Padding(5, 5, 5, 8);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABMForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ABMForm_FormClosed);
            this.Load += new System.EventHandler(this.ABMForm_Load);
            this.GrpDatosCargados.ResumeLayout(false);
            this.SPCDatosCargados.Panel1.ResumeLayout(false);
            this.SPCDatosCargados.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SPCDatosCargados)).EndInit();
            this.SPCDatosCargados.ResumeLayout(false);
            this.PanelBar.ResumeLayout(false);
            this.PanelBar.PerformLayout();
            this.tblToolbar.ResumeLayout(false);
            this.GrpCategorias.ResumeLayout(false);
            this.PNLCategorias.ResumeLayout(false);
            this.PNLDatosCargados.ResumeLayout(false);
            this.PNLCargarDatos.ResumeLayout(false);
            this.PNLToolBar.ResumeLayout(false);
            this.PNLToolBar.PerformLayout();
            this.PNLContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PixBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}


		protected System.Windows.Forms.GroupBox GrpDatosCargados;
		protected System.Windows.Forms.PictureBox PixBox;
		protected System.Windows.Forms.Button CMDNuevo;
		protected System.Windows.Forms.Button CMDEditar;
		protected System.Windows.Forms.Button CMDCancelar;
		protected System.Windows.Forms.Button CMDEliminar;
		protected System.Windows.Forms.Button CMDBuscar;
		protected System.Windows.Forms.ProgressBar PBar;
		protected System.Windows.Forms.Panel PanelBar;
		protected System.Windows.Forms.TreeView TViewCategorias;
		protected System.Windows.Forms.GroupBox GrpCategorias;
		protected System.Windows.Forms.GroupBox GrpCargaDatos;
		protected System.Windows.Forms.Button CMDConsultar;
		protected System.Windows.Forms.Panel PNLCategorias;
		protected System.Windows.Forms.SplitContainer SPCDatosCargados;
		protected System.Windows.Forms.Panel PNLDatosCargados;
		protected System.Windows.Forms.Panel PNLCargarDatos;
		protected System.Windows.Forms.Panel PNLToolBar;
		internal System.Windows.Forms.Panel PNLContainer;
		protected NJFLib.Controls.CollapsibleSplitter HorizontalSplitter;
		protected NJFLib.Controls.CollapsibleSplitter VerticalSplitter;
		private System.Windows.Forms.TableLayoutPanel tblToolbar;

	}
}