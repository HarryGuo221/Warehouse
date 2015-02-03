using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Warehouse.DB;
using System.IO;
using System.Security.Cryptography;


namespace Warehouse.Base
{  
    class Util
    {
        #region 中文汉字转换成其首字母
        /// <summary>
        /// 中文汉字转换成其首字母
        /// </summary>
        /// <param name="chineseCharacter">中文汉字</param>
        /// <returns></returns>
        public static string ChineseCharacterToLetter(string chineseCharacter)
        {
            string strLetter = "";
            if (chineseCharacter == "")
            {
                strLetter = "";
            }
            else
            {
                strLetter = GetPYM(chineseCharacter);//获得一个字符串的拼音码
            }

            return strLetter;
        }
        #endregion
                
        #region 人民币小写金额转大写金额
        /// <summary>
        /// 小写金额转大写金额
        /// </summary>
        /// <param name="Money">接收需要转换的小写金额</param>
        /// <returns>返回大写金额</returns>
        public static string ConvertMoney(Decimal intMoney)
        {
            bool isNegative = false;
            if (intMoney < 0)
            {
                isNegative = true;
                intMoney = Convert.ToDecimal(intMoney.ToString().Trim().Substring(1)); //如果是负数，先去掉负号
            }

            //金额转换程序
            string MoneyNum = "";//记录小写金额字符串[输入参数]
            string MoneyStr = "";//记录大写金额字符串[输出参数]
            string BNumStr = "零壹贰叁肆伍陆柒捌玖";//模
            string UnitStr = "万仟佰拾亿仟佰拾万仟佰拾圆角分";//模

            MoneyNum = ((long)(intMoney * 100)).ToString();
            for (int i = 0; i < MoneyNum.Length; i++)
            {
                string DVar = "";//记录生成的单个字符(大写)
                string UnitVar = "";//记录截取的单位
                for (int n = 0; n < 10; n++)
                {
                    //对比后生成单个字符(大写)
                    if (Convert.ToInt32(MoneyNum.Substring(i, 1)) == n)
                    {
                        DVar = BNumStr.Substring(n, 1);//取出单个大写字符
                        //给生成的单个大写字符加单位
                        UnitVar = UnitStr.Substring(15 - (MoneyNum.Length)).Substring(i, 1);
                        n = 10;//退出循环
                    }
                }
                //生成大写金额字符串
                MoneyStr = MoneyStr + DVar + UnitVar;
            }
            //二次处理大写金额字符串
            MoneyStr = MoneyStr + "整";
            while (MoneyStr.Contains("零分") || MoneyStr.Contains("零角") || MoneyStr.Contains("零佰") || MoneyStr.Contains("零仟")
                || MoneyStr.Contains("零万") || MoneyStr.Contains("零亿") || MoneyStr.Contains("零零") || MoneyStr.Contains("零圆")
                || MoneyStr.Contains("亿万") || MoneyStr.Contains("零整") || MoneyStr.Contains("分整"))
            {
                MoneyStr = MoneyStr.Replace("零分", "零");
                MoneyStr = MoneyStr.Replace("零角", "零");
                MoneyStr = MoneyStr.Replace("零拾", "零");
                MoneyStr = MoneyStr.Replace("零佰", "零");
                MoneyStr = MoneyStr.Replace("零仟", "零");
                MoneyStr = MoneyStr.Replace("零万", "万");
                MoneyStr = MoneyStr.Replace("零亿", "亿");
                MoneyStr = MoneyStr.Replace("亿万", "亿");
                MoneyStr = MoneyStr.Replace("零零", "零");
                MoneyStr = MoneyStr.Replace("零圆", "圆零");
                MoneyStr = MoneyStr.Replace("零整", "整");
                MoneyStr = MoneyStr.Replace("分整", "分");
            }
            if (MoneyStr == "整")
            {
                MoneyStr = "零元整";
            }
            if (isNegative == true)
                MoneyStr = "负" + MoneyStr;
            return MoneyStr;
        }
        #endregion 
        
        #region 验证输入框不能为空
        public static bool ControlTextIsNUll(Control control)
        {
            if (control is TextBox)
            {
                TextBox textBox = control as TextBox;
                if (textBox.Text.Trim() == "")
                    return true;
            }
            if (control is ComboBox)
            {
                ComboBox comboBox = control as ComboBox;
                if (comboBox.Text.Trim() == "--请选择--")
                    return true;
            } 

            return false;
        }
        #endregion

        #region 验证输入框输入规则
        /// <summary>
        /// 文本框只能输入数字
        /// </summary>
        /// <param name="textbox"></param>
        public static bool IsNumberic(TextBox textBox)
        {
            try
            {
                string str = textBox.Text.Trim();
                double d = 0;
                d = Convert.ToDouble(textBox.Text.Trim());
            }
            catch //(Exception e)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 判断一个对象是否是数字
        /// </summary>
        /// <param name="value"></param>
        public static bool ObjectIsNumberic(object value)
        {
            try
            {
                double d = Convert.ToDouble(value);
            }
            catch //(Exception e)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 文本框只能输入小于某一个数的数字
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="number">小于该数字</param>
        /// <returns></returns>
        public static bool LessOneNumber(TextBox textBox, double number)
        {
            if (IsNumberic(textBox) == false)
                return false;
            else
            {
                if (Convert.ToDouble(textBox.Text) > number)
                {
                    //MessageBox.Show("输入数据不能大于" + number + "，请重新输入！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                else
                    return true;
            }
        }
        /// <summary>
        /// 文本框只能输入若干位数字
        /// </summary>
        /// <param name="textbox"></param>
        /// <param name="number">位数</param>
        public static bool IsPhoneNumber(TextBox textBox,int number)
        {
            try
            {                
                string str = textBox.Text.Trim();
                if (str == "")
                    return false;

                long d = 0;                
                d = Convert.ToInt64(textBox.Text.Trim());

                if (d.ToString().Trim().Length != number)
                    return false;
            }
            catch //(Exception e)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 判断一个输入控件中输入字符个数是否超出限制
        /// </summary>
        /// <param name="control"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsContainCharNumber(Control control, int number)
        {
            try
            {                  
                string str = control.Text.Trim();
                
                if (str.Length > number)
                    return false;
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 判断一个字符串是否为Email格式
        /// </summary>
        /// <param name="str_Email"></param>
        /// <returns></returns>
        public static bool IsEmail(string str_Email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        #endregion 

        #region 数据类型转换
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToStr(object obj)
        {
            if (object.Equals(obj, DBNull.Value) || string.IsNullOrEmpty(obj.ToString()))
                return "";
            else
                return obj.ToString();
        }

        public static int ToInt(object obj)
        {
            if (object.Equals(obj, DBNull.Value) || object.Equals(obj, null) || string.IsNullOrEmpty(obj.ToString()))
                return 0;
            else
                return Convert.ToInt32(obj);
        }

        public static Int16 ToInt16(object obj)
        {
            if (object.Equals(obj, DBNull.Value) || object.Equals(obj, null) || string.IsNullOrEmpty(obj.ToString()))
                return 0;
            else
                return Convert.ToInt16(obj);
        }

        public static double ToDouble(object obj)
        {
            if (object.Equals(obj, DBNull.Value) || object.Equals(obj, null) || string.IsNullOrEmpty(obj.ToString()))
                return 0;
            else
                return Convert.ToDouble(obj);
        }

        public static Single ToSingle(object obj)
        {
            if (object.Equals(obj, DBNull.Value) || object.Equals(obj, null) || string.IsNullOrEmpty(obj.ToString()))
                return 0;
            else
                return Convert.ToSingle(obj);
        }

        public static bool ToBool(object obj)
        {
            if (object.Equals(obj, DBNull.Value) || object.Equals(obj, null))
                return false;
            else
                return Convert.ToBoolean(obj);
        }

        public static DateTime ToDateTime(object obj)
        {
            try
            {
                DateTime dt;
                DateTime.TryParse(Convert.ToString(obj), out dt);
                return dt;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static DateTime? ToNullDate(object obj)
        {
            if (object.Equals(obj, DBNull.Value))
                return null;
            else
                try
                {
                    DateTime dt;
                    DateTime.TryParse(Convert.ToString(obj), out dt);
                    return dt;
                }
                catch
                {
                    return null;
                }
        }

        public static int? ToNullInt(object obj)
        {
            if (object.Equals(obj, DBNull.Value) || object.Equals(obj, null) || string.IsNullOrEmpty(obj.ToString()))
                return null;
            else
                return Convert.ToInt32(obj);
        }

        public static Int16? ToNullInt16(object obj)
        {
            if (object.Equals(obj, DBNull.Value) || object.Equals(obj, null) || string.IsNullOrEmpty(obj.ToString()))
                return null;
            else
                return Convert.ToInt16(obj);
        }

        public static double? ToNulldouble(object obj)
        {
            if (object.Equals(obj, DBNull.Value) || object.Equals(obj, null) || string.IsNullOrEmpty(obj.ToString()))
                return null;
            else
                return Convert.ToDouble(obj);
        }

        public static Single? ToNullSingle(object obj)
        {
            if (object.Equals(obj, DBNull.Value) || object.Equals(obj, null) || string.IsNullOrEmpty(obj.ToString()))
                return null;
            else
                return Convert.ToSingle(obj);
        }
        #endregion   

        #region 获得一个字符串的拼音码
        /// <summary> 
        /// 获得一个字符串的拼音码 
        /// </summary> 
        /// <param   name= "_str "> 中文字符串 </param> 
        /// <returns> 返回字符串的拼音码 </returns> 
        public static string GetPYM(string _str) 
        { 
            Encoding gb2312=Encoding.GetEncoding("gb2312"); 
            byte[] buf=gb2312.GetBytes(_str); 
            StringBuilder sbResult = new StringBuilder(""); 
            for(int i = 0; i < buf.Length;) 
            { 
                if(i+1==buf.Length) 
                { 
                    if((buf[i] >= '!')&&(buf[i] <= '~')) 
                    { 
                        sbResult.Append((char)buf[i]); 
                    } 
                    break; 
                } 

                if((buf[i]> 128)&&(buf[i+1]> 128)) 
                { 
                    sbResult.Append(GetPYM(buf[i],buf[i+1])); 
                    i+=2; 
                } 
                else 
                { 
                    if((buf[i] >= '!') && (buf[i] <='~')) 
                    { 
                        sbResult.Append((char)buf[i]); 
                    } 
                    i++; 
                } 
            } 

            return  sbResult.ToString(); 
        }
        /// <summary> 
        /// 获得一个双字节字符的拼音码 
        /// </summary> 
        /// <param name= "_byte0 "> 第一位 </param> 
        /// <param name= "_byte1 "> 第二位 </param> 
        /// <returns> 拼音码（生母）</returns> 
        private static char GetPYM(byte _byte0, byte _byte1) 
        { 
            int int0=_byte0-176; 
            int int1=_byte1-161; 
            int intASCII=int0*94+int1; 

            if(int0 < 0) return  ' '; 
            if(int0 > 71) return  ' '; 
            if(int1 < 0) return  ' '; 
            if(int1 > 93) return  ' '; 

            //处理一级字库 
            if((0 <=intASCII)&&(intASCII <36)) return   'A'; 
            if(intASCII <220)return   'B'; 
            if(intASCII <453)return   'C'; 
            if(intASCII <637)return   'D'; 
            if(intASCII <659)return   'E'; 
            if(intASCII <784)return   'F'; 
            if(intASCII <939)return   'G'; 
            if(intASCII <1120)return   'H'; 
            if(intASCII <1415)return   'J'; 
            if(intASCII <1515)return   'K'; 
            if(intASCII <1763)return   'L'; 
            if(intASCII <1914)return   'M'; 
            if(intASCII <1995)return   'N'; 
            if(intASCII <2003)return   'O'; 
            if(intASCII <2125)return   'P'; 
            if(intASCII <2282)return   'Q'; 
            if(intASCII <2341)return   'R'; 
            if(intASCII <2627)return   'S'; 
            if(intASCII <2783)return   'T'; 
            if(intASCII <2903)return   'W'; 
            if(intASCII <3126)return   'X'; 
            if(intASCII <3432)return   'Y'; 
            if(intASCII==3496)return   'N'; 
            if(intASCII <3757)return   'Z'; 

            //处理二级字库 
            string[]   arrChars=new   string[32]; 
            arrChars[0]= "CJWGNSPGCGNE   Y   BTYYZDXYKYGT   JNNJQMBSGZSCYJSYY   PGKBZGY   YWYKGKLJSWKPJQHY   W   DZLSGMRYPYWWCCKZNKYYG "; 
            arrChars[1]= "TTNJJEYKKZYTCJNMCYLQLYPYQFQRPZSLWBTGKJFYXJWZLTBNCXJJJJZXDTTSQZYCDXXHGCK   PHFFSS   YBGMXLPBYLL   HLX "; 
            arrChars[2]= "S   ZM   JHSOJNGHDZQYKLGJHXGQZHXQGKEZZWYSCSCJXYEYXADZPMDSSMZJZQJYZC   J   WQJBDZBXGZNZCPWHKXHQKMWFBPBY "; 
            arrChars[3]= "DTJZZKQHYLYGXFPTYJYYZPSZLFCHMQSHGMXXSXJ     DCSBBQBEFSJYHXWGZKPYLQBGLDLCCTNMAYDDKSSNGYCSGXLYZAYBN "; 
            arrChars[4]= "PTSDKDYLHGYMYLCXPY   JNDQJWXQXFYYFJLEJPZRXCCQWQQSBZKYMGPLBMJRQCFLNYMYQMSQYRBCJTHZTQFRXQHXMJJCJLX "; 
            arrChars[5]= "XGJMSHZKBSWYEMYLTXFSYDSGLYCJQXSJNQBSCTYHBFTDCYZDJWYGHQFRXWCKQKXEBPTLPXJZSRMEBWHJLBJSLYYSMDXLCL "; 
            arrChars[6]= "QKXLHXJRZJMFQHXHWYWSBHTRXXGLHQHFNM   YKLDYXZPWLGG   MTCFPAJJZYLJTYANJGBJPLQGDZYQYAXBKYSECJSZNSLYZH "; 
            arrChars[7]= "ZXLZCGHPXZHZNYTDSBCJKDLZAYFMYDLEBBGQYZKXGLDNDNYSKJSHDLYXBCGHXYPKDQMMZNGMMCLGWZSZXZJFZNMLZZTHCS "; 
            arrChars[8]= "YDBDLLSCDDNLKJYKJSYCJLKOHQASDKNHCSGANHDAASHTCPLCPQYBSDMPJLPZJOQLCDHJJYSPRCHN   NNLHLYYQYHWZPTCZG "; 
            arrChars[9]= "WWMZFFJQQQQYXACLBHKDJXDGMMYDJXZLLSYGXGKJRYWZWYCLZMSSJZLDBYDCPCXYHLXCHYZJQ     QAGMNYXPFRKSSBJLYXY "; 
            arrChars[10]= "SYGLNSCMHSWWMNZJJLXXHCHSY     CTXRYCYXBYHCSMXJSZNPWGPXXTAYBGAJCXLY   DCCWZOCWKCCSBNHCPDYZNFCYYTYCKX "; 
            arrChars[11]= "KYBSQKKYTQQXFCWCHCYKELZQBSQYJQCCLMTHSYWHMKTLKJLYCXWHYQQHTQH   PQ   QSCFYMMDMGBWHWLGSLLYSDLMLXPTHMJ "; 
            arrChars[12]= "HWLJZYHZJXHTXJLHXRSWLWZJCBXMHZQXSDZPMGFCSGLSXYMQSHXPJXWMYQKSMYPLRTHBXFTPMHYXLCHLHLZYLXGSSSSTCL "; 
            arrChars[13]= "SLTCLRPBHZHXYYFHB   GDMYCNQQWLQHJJ   YWJZYEJJDHPBLQXTQKWHLCHQXAGTLXLJXMSL   HTZKZJECXJCJNMFBY   SFYWYB "; 
            arrChars[14]= "JZGNYSDZSQYRSLJPCLPWXSDWEJBJCBCNAYTWGMPABCLYQPCLZXSBNMSGGFNZJJBZSFZYNDXHPLQKZCZWALSBCCJX   YZHWK "; 
            arrChars[15]= "YPSGXFZFCDKHJGXDLQFSGDSLQWZKXTMHSBGZMJZRGLYJBPMLMSXLZJQSHZYJ   ZYDJWBMJKLDDPMJEGXYHYLXHLQYQHKYCW "; 
            arrChars[16]= "CJMYYXNATJHYCCXZPCQLBZWWYTWBQCMLPMYRJCCCXFPZNZZLJPLXXYZTZLGDLDCKLYRZZGQTGJHHGJLJAXFGFJZSLCFDQZ "; 
            arrChars[17]= "LCLGJDJCSNCLLJPJQDCCLCJXMYZFTSXGCGSBRZXJQQCTZHGYQTJQQLZXJYLYLBCYAMCSTYLPDJBYREGKJZYZHLYSZQLZNW "; 
            arrChars[18]= "CZCLLWJQJJJKDGJZOLBBZPPGLGHTGZXYGHZMYCNQSYCYHBHGXKAMTXYXNBSKYZZGJZLQJDFCJXDYGJQJJPMGWGJJJPKQSB "; 
            arrChars[19]= "GBMMCJSSCLPQPDXCDYYKY   CJDDYYGYWRHJRTGZNYQLDKLJSZZGZQZJGDYKSHPZMTLCPWNJAFYZDJCNMWESCYGLBTZCGMSS "; 
            arrChars[20]= "LLYXQSXSBSJSBBSGGHFJLYPMZJNLYYWDQSHZXTYYWHMZYHYWDBXBTLMSYYYFSXJC   TXXLHJHF   SXZQHFZMZCZTQCXZXRTT "; 
            arrChars[21]= "DJHNNYZQQMNQDMMG   YTXMJGDHCDYZBFFALLZTDLTFXMXQZDNGWQDBDCZJDXBZGSQQDDJCMBKZFFXMKDMDSYYSZCMLJDSYN "; 
            arrChars[22]= "SPRSKMKMPCKLGDBQTFZSWTFGGLYPLLJZHGJ   GYPZLTCSMCNBTJBQFKTHBYZGKPBBYMTDSSXTBNPDKLEYCJNYDDYKZTDHQH "; 
            arrChars[23]= "SDZSCTARLLTKZLGECLLKJLQJAQNBDKKGHPJTZQKSECSHALQFMMGJNLYJBBTMLYZXDCJPLDLPCQDHZYCBZSCZBZMSLJFLKR "; 
            arrChars[24]= "ZJSNFRGJHXPDHYJYBZGDLJCSEZGXLBLHYXTWMABCHECMWYJYZLLJJYHLG   DJLSLYGKDZPZXJYYZLWCXSZFGWYYDLYHCLJS "; 
            arrChars[25]= "CMBJHBLYZLYCBLYDPDQYSXQZBYTDKYYJY   CNRJMPDJGKLCLJBCTBJDDBBLBLCZQRPPXJCGLZCSHLTOLJNMDDDLNGKAQHQH "; 
            arrChars[26]= "JHYKHEZNMSHRP   QQJCHGMFPRXHJGDYCHGHLYRZQLCYQJNZSQTKQJYMSZSWLCFQQQXYFGGYPTQWLMCRNFKKFSYYLQBMQAMM "; 
            arrChars[27]= "MYXCTPSHCPTXXZZSMPHPSHMCLMLDQFYQFSZYJDJJZZHQPDSZGLSTJBCKBXYQZJSGPSXQZQZRQTBDKYXZKHHGFLBCSMDLDG "; 
            arrChars[28]= "DZDBLZYYCXNNCSYBZBFGLZZXSWMSCCMQNJQSBDQSJTXXMBLTXZCLZSHZCXRQJGJYLXZFJPHY   ZQQYDFQJJLZZNZJCDGZYG "; 
            arrChars[29]= "CTXMZYSCTLKPHTXHTLBJXJLXSCDQXCBBTJFQZFSLTJBTKQBXXJJLJCHCZDBZJDCZJDCPRNPQCJPFCZLCLZXZDMXMPHJSGZ "; 
            arrChars[30]= "GSZZQLYLWTJPFSYAXMCJBTZYYCWMYTCSJJLQCQLWZMALBXYFBPNLSFHTGJWEJJXXGLLJSTGSHJQLZFKCGNNDSZFDEQFHBS "; 
            arrChars[31]= "AQTGYLBXMMYGSZLDYDQMJJRGBJTKGDHGKBLQKBDMBYLXWCXYTTYBKMRTJZXQJBHLMHMJJZMQASLDCYXYQDLQCAFYWYXQHZ "; 
            return arrChars[int0-40][int1];
        }
        #endregion

        #region 把一个MenuStrip控件中的菜单项转化为一个树TreeView
        /// <summary>
        /// 把一个MenuStrip控件中的菜单项转化为一个树TreeView
        /// </summary>
        /// <param name="treeViewMenus"></param>
        /// <param name="menuStripMain"></param>
        public static void MenuStripToTreeView(TreeView treeViewMenus, MenuStrip menuStripMain)
        {
            treeViewMenus.Nodes.Clear();//清空导航菜单

            //调用GetMenu方法，将menuStrip1控件的子菜单添加到treeView1控件中
            Util.GetMenu(treeViewMenus, menuStripMain);

            treeViewMenus.ExpandAll();
        }
        #endregion

        #region  将MenuStrip控件中的信息添加到TreeView控件中
        /// <summary>       
        /// 将MenuStrip控件中的信息添加到TreeView控件中        
        /// </summary>        
        /// <param name="treeV">TreeView控件</param>       
        /// <param name="MenuS">MenuStrip控件</param>        
        public static void GetMenu(TreeView treeV, MenuStrip MenuS)
        {
            for (int i = 0; i < MenuS.Items.Count; i++) //遍历MenuStrip组件中的一级菜单项            
            {
                string itemText = MenuS.Items[i].Text.Trim();
                if (itemText.Contains('('))
                    itemText = itemText.Remove(itemText.IndexOf('('));
               
                //将一级菜单项的名称添加到TreeView组件的根节点中，并设置当前节点的子节点newNode1                
                TreeNode newNode1 = treeV.Nodes.Add(itemText);
                //将当前菜单项的所有相关信息存入到ToolStripDropDownItem对象中                
                ToolStripDropDownItem newmenu = (ToolStripDropDownItem)MenuS.Items[i];
                //判断当前菜单项中是否有二级菜单项                
                if (newmenu.HasDropDownItems && newmenu.DropDownItems.Count > 0)
                {
                    for (int j = 0; j < newmenu.DropDownItems.Count; j++) //遍历二级菜单项                    
                    {
                        if (newmenu.DropDownItems[j] is ToolStripDropDownItem && !(newmenu.DropDownItems[j] is ToolStripSeparator))////其它类型的菜单项不考虑
                        {
                            string nItemText = newmenu.DropDownItems[j].Text.Trim();
                            if (nItemText.Contains('('))
                                nItemText = nItemText.Remove(nItemText.IndexOf('('));
                            //将二级菜单名称添加到TreeView组件的子节点newNode1中，并设置当前节点的子节点newNode2                        
                            TreeNode newNode2 = newNode1.Nodes.Add(nItemText);
                            //将当前菜单项的所有相关信息存入到ToolStripDropDownItem对象中                          
                            ToolStripDropDownItem newmenu2 = (ToolStripDropDownItem)newmenu.DropDownItems[j];
                            //判断二级菜单项中是否有三级菜单项                        
                            if (newmenu2.HasDropDownItems && newmenu2.DropDownItems.Count > 0)
                                for (int p = 0; p < newmenu2.DropDownItems.Count; p++)    //遍历三级菜单项                                
                                    //将三级菜单名称添加到TreeView组件的子节点newNode2中  
                                    if (!(newmenu2.DropDownItems[p] is ToolStripSeparator))
                                    {
                                        string nItemText2 = newmenu2.DropDownItems[p].Text.Trim();
                                        if (nItemText2.Contains('('))
                                            nItemText2 = nItemText2.Remove(nItemText2.IndexOf('('));

                                        newNode2.Nodes.Add(nItemText2);
                                    }
                        }                        
                    }
                }
            }
        }
        #endregion

        #region MD5 加密
        /// <summary>
        /// 将指定字符串进行MD5加密
        /// </summary>
        /// <param name="oldStr">需要加密的字符串</param>
        /// <returns>加密后的字符串(32位)</returns>
        public static string GetMD5str(string oldStr)
        {
            //将输入转换为ASCII 字符编码
            ASCIIEncoding enc = new ASCIIEncoding();
            //将字符串转换为字节数组
            byte[] buffer = enc.GetBytes(oldStr);
            //创建MD5实例
            MD5 md5 = new MD5CryptoServiceProvider();
            //进行MD5加密
            byte[] hash = md5.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            //拼装加密后的字符
            for (int i = 0; i < hash.Length; i++)
            {
                sb.AppendFormat("{0:x2}", hash[i]);
            }
            //输出加密后的字符串
            return sb.ToString();
        }
        #endregion

        #region 加密解密
        public static readonly string myKey = "abcdefg";

        #region 加密方法
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="pToEncrypt">需要加密字符串</param>
        /// <param name="sKey">密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string pToEncrypt, string sKey)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                //把字符串放到byte数组中


                //原来使用的UTF8编码，我改成Unicode编码了，不行
                byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);

                //建立加密对象的密钥和偏移量


                //使得输入密码必须输入英文文本
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                StringBuilder ret = new StringBuilder();
                foreach (byte b in ms.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }
                ret.ToString();
                return ret.ToString();
            }
            catch //(Exception ex)
            {

            }

            return "";
        }
        #endregion

        #region 解密方法
        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="pToDecrypt">需要解密的字符串</param>
        /// <param name="sKey">密匙</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt(string pToDecrypt, string sKey)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
                for (int x = 0; x < pToDecrypt.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }

                //建立加密对象的密钥和偏移量，此值重要，不能修改
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象
                StringBuilder ret = new StringBuilder();
                return System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            catch //(Exception ex)
            {

            }
            return "";
        }
        #endregion
        #endregion 

        #region 文本对齐
        /// <summary>
        /// 文本右对齐
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string FormatStringRight(String s, int length)//形如 ****abc
        {
            return "                                ".Substring(0, length - StringRealLenght(s)) + s;
        }
        /// <summary>
        /// 文本左对齐
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string FormatStringLeft(String s, int length) //形如 abc****
        {
            return s + "                                            ".Substring(0, length - StringRealLenght(s));
        }
        public static int StringRealLenght(string str)
        {
            System.Text.ASCIIEncoding n = new System.Text.ASCIIEncoding();
            byte[] b = n.GetBytes(str);
            int length = 0;                          // l 为字符串的实际长度
            for (int i = 0; i <= b.Length - 1; i++)
            {
                if (b[i] == 63)             //判断是否为汉字或全脚符号
                {
                    length++;
                }
                length++;
            }
            return length;
        }
        #endregion

        #region //获取Config.ini中的配置信息
        /// <summary>
        /// 获取Config.ini中的配置信息
        /// </summary>
        /// <param name="ip_"></param>
        /// <param name="db_"></param>
        /// <param name="ver_"></param>
        public void GetInf(ref string ip_, ref string db_, ref string db_jitoa, ref string ver_)
        {
            string file_ = System.Environment.CurrentDirectory + "\\config.ini";
            string str = "";
            int pos_ = 0;
            StreamReader sr = File.OpenText(file_);
            while (true)
            {
                str = sr.ReadLine();
                if (str != null)
                {
                    if (str.ToLower().IndexOf("servername=") != -1)
                    {
                        pos_ = str.IndexOf('=');
                        ip_ = str.Substring(pos_ + 1, str.Length - pos_ - 1);
                    }
                    if (str.ToLower().IndexOf("database=") != -1)
                    {
                        pos_ = str.IndexOf('=');
                        db_ = str.Substring(pos_ + 1, str.Length - pos_ - 1);
                    }
                    if (str.ToLower().IndexOf("database_jitoa=") != -1)
                    {
                        pos_ = str.IndexOf('=');
                        db_jitoa = str.Substring(pos_ + 1, str.Length - pos_ - 1);
                    }
                    if (str.ToLower().IndexOf("version=") != -1)
                    {
                        pos_ = str.IndexOf('=');
                        ver_ = str.Substring(pos_ + 1, str.Length - pos_ - 1);
                    }
                }
                else
                    break;
            }
            sr.Close();
            sr.Dispose();

        }
        /// <summary>
        /// 从服务器读取执行文件的上传时间
        /// </summary>
        /// <returns></returns>
        public string get_version()
        {
            string vernew_ = "";
            string sql_ = "select UploadTime from T_SysConfig";
            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = db.Get_Dt(sql_);

            if (dt == null || dt.Rows.Count <= 0)
                return "";
            vernew_ = dt.Rows[0]["UploadTime"].ToString().Trim();
                                   
            return vernew_;
        }
        #endregion

        #region 物料类型转换
        /// <summary>
        /// 获得物料类型（int）
        /// </summary>
        /// <param name="typeName">类型名</param>
        /// <returns></returns>
        public static int GetMatType(string typeName)
        {            
            switch (typeName)
            {
                case "0新机":
                    {
                        return 0;
                        break;
                    }
                case "1旧机":
                    {
                        return 1;
                        break;
                    }
                case "2样机":
                    {
                        return 2;
                        break;
                    }
                default:
                    {
                        return -1;
                        break;
                    }
            }
        }
        /// <summary>
        /// 获得物料类型名称
        /// </summary>
        /// <param name="type">类型(0,1,2)</param>
        /// <returns></returns>
        public static string GetMatTypeName(int type)
        {
            string intMatTypeName = "";
            switch (type)
            {
                case 0:
                    {
                        return "0新机";
                        break;
                    }
                case 1:
                    {
                        return "1旧机";
                        break;
                    }
                case 2:
                    {
                        return "2样机";
                        break;
                    }
                default:
                    {
                        return "";
                        break;
                    }
            }
            return intMatTypeName;
        }
        #endregion

        #region 机器拆分与还原操作类型的数据转换
        /// <summary>
        /// 获得操作类型（int）
        /// </summary>
        /// <param name="typeName">类型名</param>
        /// <returns></returns>
        public static string GetOperateType(string typeName)
        {
            switch (typeName)
            {
                case "0拆分":
                    {
                        return "0";
                        break;
                    }
                case "1还原":
                    {
                        return "1";
                        break;
                    }
                default:
                    {
                        return "-1";
                        break;
                    }
            }
        }
        /// <summary>
        /// 获得操作类型名称
        /// </summary>
        /// <param name="type">类型(0,1,2)</param>
        /// <returns></returns>
        public static string GetOperateTypeName(int type)
        {
            string intOperateTypeName = "";
            switch (type)
            {
                case 0:
                    {
                        return "0拆分";
                        break;
                    }
                case 1:
                    {
                        return "1还原";
                        break;
                    }
                default:
                    {
                        return "";
                        break;
                    }
            }
            return intOperateTypeName;
        }
        #endregion

        #region 使控件中包含的输入控件的值置空
        public static void ClearControlText(Control control)
        {
            foreach (Control o in control.Controls)
            {
                if (o is TextBox)
                {
                    TextBox textBox = o as TextBox;
                    textBox.Text = "";                        
                }
                if (o is ComboBox)
                {
                    ComboBox comboBox = o as ComboBox;
                    comboBox.SelectedIndex = 0;                       
                }
            }
        }
        #endregion

        #region 使一个控件中的子控件在按下Enter键后，按Tab键次序后移
        public static void textBox_KeyUp(Control control, object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                control.SelectNextControl(control, true, true, false, true);
            }
        }
        public static void Control_keypress( KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SendKeys.Send("{TAB}");//也可以使用这个代替SelectNextControl
            }
        }
        #endregion

        #region 判断一个字符串是否全部为数字0-9
        /// <summary>
        /// 判断一个字符串是否全部为数字0-9
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool StringIsNum(string str)
        {
            if (str == "")
                return false;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] < '0' || str[i] > '9')
                    return false;
            }
            return true;
        }
        #endregion

        #region 克隆一个DataGridview
        /// <summary>
        /// 克隆一个DataGridview
        /// </summary>
        /// <param name="datagridview">DataGridview</param>
        /// <returns></returns>
        public static DataGridView CopyNewDataGridViewAllRows(DataGridView datagridview)
        {
            DataGridView datagridview2 = new DataGridView();
            for (int i = 0; i < datagridview.ColumnCount; i++)
            {
                DataGridViewColumn dgvcol = (DataGridViewColumn)datagridview.Columns[i].Clone();
                datagridview2.Columns.Add(dgvcol);

            }
            object[] objs = new object[datagridview.Columns.Count];
            for (int i = 0; i < datagridview.Rows.Count; i++)
            {
                for (int j = 0; j < datagridview.Columns.Count; j++)
                {
                    objs[j] = datagridview.Rows[i].Cells[j].Value;
                }
                datagridview2.Rows.Add(objs);
            }
            return datagridview2;
        }
        /// <summary>
        /// 克隆一个DataGridview
        /// </summary>
        /// <param name="datagridview">DataGridview</param>
        /// <returns></returns>
        public static DataGridView CopyNewDataGridViewSelectedRows(DataGridView datagridview)
        {
            DataGridView datagridview2 = new DataGridView();
            for (int i = 0; i < datagridview.ColumnCount; i++)
            {
                DataGridViewColumn dgvcol = (DataGridViewColumn)datagridview.Columns[i].Clone();
                datagridview2.Columns.Add(dgvcol);

            }
            object[] objs = new object[datagridview.Columns.Count];
            for (int i = 0; i < datagridview.SelectedRows.Count; i++)
            {
                for (int j = 0; j < datagridview.Columns.Count; j++)
                {
                    objs[j] = datagridview.SelectedRows[i].Cells[j].Value;
                }
                datagridview2.Rows.Add(objs);
            }
            return datagridview2;
        }
        #endregion

        /// <summary>
        /// 获得型如（201107）的下一个月份
        /// </summary>
        /// <param name="curMonth"></param>
        /// <returns></returns>
        public static string GetNextMonth(string curMonth)
        {
            string str = "";
            curMonth = curMonth.Insert(4, "-");
            DateTime dateTime = Convert.ToDateTime(curMonth);

            dateTime = DateTime.Parse(dateTime.AddMonths(1).ToShortDateString());

            str = dateTime.ToString("yyyy-MM-dd").Trim().Substring(0, 7);
            str = str.Remove(4, 1);

            return str;
        }


        //对取出元素值为空的赋零值
        public static double YNdbnull(string DTelement)
        {
            object o = DTelement;
            if (o != DBNull.Value && DTelement != "")
            {
                return Convert.ToDouble(DTelement);
            }
            else
                return 0;
        }

        //转化SQL语句中，字段值的格式
        public static string Get_Fields_Format(string Sourstr, string lx)
        {
            string RetStr = "";
            lx = lx.ToUpper().Trim();
            if (lx != "N")   //非数值型
            {
                RetStr = "'" + Sourstr.Trim() + "'";
            }
            else
            {
                if (Sourstr.Trim() == "")
                    RetStr = "null";
                else
                    RetStr = Sourstr.Trim();
            }
            return RetStr;
       }


        public static bool Is_Equal(double n1,double n2)
        { 
           if  (Math.Abs(n1-n2)<0.00001)
               return true;
           else
               return false;
        }

    }
}
