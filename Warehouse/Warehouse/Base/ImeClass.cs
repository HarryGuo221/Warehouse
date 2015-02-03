using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using   System.Windows.Forms;

namespace Warehouse.Base
{
    class ImeClass:System.Windows.Forms.Form 
    {
          ///   <summary> 
                ///   做一个标记,避免重复设置Enter事件 
                ///   </summary> 
                private   bool   m_AttachProcessed   =   false; 
                ///   <summary> 
                ///   在Load事件中遍历控件,如果是文本框,自动切换到半角状态 
                ///   </summary> 
                ///   <param   name= "e "> </param> 
                protected   override   void   OnLoad(EventArgs   e) 
                { 
                        base.OnLoad(e); 
                        if   (!m_AttachProcessed) 
                        { 
                                SetImeToHangul(this.Controls); 
                                m_AttachProcessed   =   true; 
                        } 
                } 

                ///   <summary> 
                ///   通过递归,遍历当前窗口的全部控件 
                ///   </summary> 
                ///   <param   name= "p_Controls "> 容器 </param> 
                public  void   SetImeToHangul(System.Windows.Forms.Control.ControlCollection   p_Controls) 
                { 
                        foreach   (System.Windows.Forms.Control   ctl   in   p_Controls) 
                        { 
                            FlowLayoutPanel   flpan   =   ctl   as   FlowLayoutPanel; 
                            if   (flpan   !=   null) 
                            { 
                                SetImeToHangul(flpan.Controls); 
                                continue; 
                            } 
                            GroupBox   grp   =   ctl   as   GroupBox; 
                            if   (grp   !=   null) 
                        { 
                                SetImeToHangul(grp.Controls); 
                                continue; 
                            } 

                            Panel   pan=   ctl   as   Panel; 
                            if   (pan   !=   null) 
                            { 
                                    SetImeToHangul(pan.Controls); 
                                    continue; 
                            } 
                            TabControl   tabcontrol   =   ctl   as   TabControl; 
                            if   (tabcontrol   !=   null) 
                            { 
                                    SetImeToHangul(tabcontrol.Controls); 
                                    continue; 
                            } 
                            TabPage   tabpage   =   ctl   as   TabPage; 
                            if   (tabpage   !=   null) 
                            { 
                                    SetImeToHangul(tabpage.Controls); 
                                    continue; 
                            } 
                            TableLayoutPanel   tlpan   =   ctl   as   TableLayoutPanel; 
                            if(tlpan!=null) 
                                { 
                                        SetImeToHangul(tlpan.Controls); 
                                    continue; 
                            } 
                            //文本框进入时,自动切换到半角,如果要控制其他可输入控件,参照下面的代码完成 
                                TextBox   txtbox   =   ctl   as   TextBox; 
                            if   (txtbox   !=   null) 
                            { 
                                    txtbox.Enter   +=   new   EventHandler(ControlEnter_Enter); 
                            } 
                      } 
                } 
                private     void   ControlEnter_Enter(object   sender,   EventArgs   e) 
                { 
                        Control   ctl   =   sender   as   Control; 
                        if   (ctl   ==   null) 
                                return; 
                        if   (ctl.ImeMode   !=   ImeMode.Hangul) 
                                ctl.ImeMode   =   ImeMode.Hangul; 
                } 
        }//class 
}

