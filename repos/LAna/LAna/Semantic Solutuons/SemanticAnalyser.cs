using System;
using System.Collections.Generic;
using System.Text;

namespace LAna.Semantic_Solutuons
{
    static class globalVar
    {
        public static List<string> lexTokens = new List<string>();
        public static List<string> tokenType = new List<string>();

        public static int maxCount;
        public static int ittr=0;
    } //class for declaring global variable for tokens i.e lexTokens
    static class tokensGet
    {
        public static void Func(List<string> tokens, List<string> type)
        {
            
            globalVar.lexTokens = tokens;
            globalVar.tokenType = type;
            globalVar.maxCount=globalVar.lexTokens.Count;
        }

    } //class for method for setting global variable lexTokens from analyser class


     
    class syntaxAnalyser
    {
        public bool Start()
        {
            if (def())
            {
                if (cat())
                {
                    if (globalVar.lexTokens[globalVar.ittr] == "class")
                    {
                        globalVar.ittr++;
                        if (globalVar.tokenType[globalVar.ittr] == "identifier")
                        {
                            globalVar.ittr++;
                            if (inh())
                            {
                                if (globalVar.lexTokens[globalVar.ittr] == "{")
                                {
                                    globalVar.ittr++;
                                    if (cBody())
                                    {
                                        if (globalVar.lexTokens[globalVar.ittr]=="public")
                                        {
                                            globalVar.ittr++;
                                            if (globalVar.lexTokens[globalVar.ittr]=="static")
                                            {
                                                globalVar.ittr++;
                                                if (globalVar.lexTokens[globalVar.ittr]=="void")
                                                {
                                                    globalVar.ittr++;
                                                    if (globalVar.lexTokens[globalVar.ittr]=="main")
                                                    {
                                                        globalVar.ittr++;
                                                        if (globalVar.lexTokens[globalVar.ittr]=="(")
                                                        {
                                                            globalVar.ittr++;
                                                            if (globalVar.lexTokens[globalVar.ittr] == ")")
                                                            {
                                                                globalVar.ittr++;
                                                                if (globalVar.lexTokens[globalVar.ittr] == "{")
                                                                {
                                                                    globalVar.ittr++;
                                                                    if (mST())
                                                                    {
                                                                        if (globalVar.lexTokens[globalVar.ittr] == "}")
                                                                        {
                                                                            globalVar.ittr++;
                                                                            if (globalVar.lexTokens[globalVar.ittr] == "}")
                                                                            {
                                                                                if (def())
                                                                                {
                                                                                    Console.WriteLine("parsed succ");
                                                                                    return true;
                                                                                }
                                                                                else
                                                                                {
                                                                                    Console.WriteLine("unsucc");
                                                                                    return false;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                return false;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            return false;
                                                                        }

                                                                    }
                                                                    else
                                                                    {
                                                                        return false;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    return false;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                return false;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            return false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        return false;
                                                    }
                                                }
                                                else
                                                {
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool body()
        {
            if (globalVar.lexTokens[globalVar.ittr]==";")
            {
                globalVar.ittr++;
                return true;
            }
            else if (globalVar.lexTokens[globalVar.ittr] == "{")
            {
                globalVar.ittr++;
                if (mST())
                {
                    if (globalVar.lexTokens[globalVar.ittr] == "}")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if(sSt())
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool mST()
        {

            if (sSt())
            {
                if (mST())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        // woritten
        public bool sSt()
        {
            if (whileSt())
            {
                return true;
            }
            else if (ifSt())
            {
                return true;
            }
            else if(varDec())
            {
                return true;
            }
            else if (funDec())
            {
                return true;
            }
            else if (swSt())
            {
                return true;
            }
            else if (incdecSt())
            {
                return true;
            }
            else
            {
                return true;
            }
        } //need to add

        public bool cBody()
        {
            if (varDec())
            {
                if (cBody())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (funDec())
            {
                if (cBody())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                //return true;
                return true;//change kiya h
            }
        }

        public bool funDec()//
        {
            if (AM())
            {
                if (cat())
                {
                    if (retType()) //write return type
                    {
                        if (id())
                        {
                            if (globalVar.lexTokens[globalVar.ittr]=="(")
                            {
                                globalVar.ittr++;
                                if (pList())
                                {
                                    if (globalVar.lexTokens[globalVar.ittr]==")")
                                    {
                                        globalVar.ittr++;
                                        if (globalVar.lexTokens[globalVar.ittr]=="{")
                                        {
                                            globalVar.ittr++;
                                            if (mST())
                                            {
                                                if (globalVar.lexTokens[globalVar.ittr]=="}")
                                                {
                                                    globalVar.ittr++;
                                                    return true;
                                                }
                                                else
                                                {
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }

                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        public bool pList()//written
        {
            if (dataType())
            {
                if (id())
                {
                    if (pList())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if(globalVar.lexTokens[globalVar.ittr]==",")
            {
                if (pList())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public bool retType() //  written
        {
            if (dataType())
            {
                return true;

            }
            else if(globalVar.lexTokens[globalVar.ittr]=="void")
            {
                globalVar.ittr++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool varDec()
        {
            if (dataType())
            {
                if (globalVar.tokenType[globalVar.ittr]== "identifier")
                {
                    globalVar.ittr++;
                    if (L1())
                    {
                        if (L2())
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool dataType()
        {
            if (globalVar.lexTokens[globalVar.ittr]=="int")
            {
                return true;
            }
            else if (globalVar.lexTokens[globalVar.ittr] == "string")
            {
                return true;
            }
            else if (globalVar.lexTokens[globalVar.ittr] == "char")
            {
                return true;
            }
            else if (globalVar.lexTokens[globalVar.ittr] == "float")
            {
                return true;
            }
            else if (globalVar.lexTokens[globalVar.ittr] == "bool")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool L2()
        {
            if (globalVar.lexTokens[globalVar.ittr]== ";")
            {
                return true;
            }
            else if (globalVar.lexTokens[globalVar.ittr]==",")
            {
                globalVar.ittr++;
                if (id())
                {
                    return true;
                }
                else if (L1())
                {
                    if (L2())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool oE() //OE not written
        {
            return true;
        }

        public bool L1()
        {
            if (globalVar.lexTokens[globalVar.ittr]=="=")
            {
                globalVar.ittr++;
                if (oE())
                {
                    return true;
                }
                else if (idConst())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
            
            
        }//wirtten

        private bool idConst()
        {
            throw new NotImplementedException();
        } //not written

        public bool inh()
        {
            if (globalVar.lexTokens[globalVar.ittr]==":")
            {
                globalVar.ittr++;
                if (globalVar.tokenType[globalVar.ittr]=="identifier")
                {
                    globalVar.ittr++;
                    if (inh())
                    {
                        //globalVar.ittr++;//maslah yhan arha h
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return true;
            }
        }//written

        public bool id()
        {
            if (globalVar.tokenType[globalVar.ittr]=="identifier")
            {
                return true;
            }
            return false;
        }//written
        
        public bool cat()
        {
            if (globalVar.lexTokens[globalVar.ittr] == "static")
            {
                globalVar.ittr++;
                return true;
            }
            else if (globalVar.lexTokens[globalVar.ittr] == "abstract")
            {
                globalVar.ittr++;
                return true;
            }
            else if (globalVar.lexTokens[globalVar.ittr] == "sealed")
            {
                globalVar.ittr++;
                return true;
            }
            else
            {
                return true;
            }
        }//written

        public bool def()
        {
            if (classDef())
            {

                if (def())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

        }//written

        public bool classDef()
        {
            //List<string> classDef = new List<string>() { AM(), cat(), "class", id(), inh(), "{", cBody(), "}" };
            if (AM())
            {
                if (cat())
                {
                    if (globalVar.lexTokens[globalVar.ittr]=="class")
                    {
                        globalVar.ittr++;
                        if (globalVar.tokenType[globalVar.ittr]=="identifier")
                        {
                            globalVar.ittr++;
                            if (inh())
                            {
                                if (globalVar.lexTokens[globalVar.ittr]=="{")
                                {
                                    globalVar.ittr++;
                                    if (cBody())
                                    {
                                        if (globalVar.lexTokens[globalVar.ittr]=="}")
                                        {
                                            globalVar.ittr++;
                                            return true;
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }//written

        public bool AM()
        {
            if (globalVar.lexTokens[globalVar.ittr]=="public")
            {
                globalVar.ittr++;
                return true;
            }
            else if (globalVar.lexTokens[globalVar.ittr]=="private")
            {
                globalVar.ittr++;
                return true;
            }
            else if (globalVar.lexTokens[globalVar.ittr]=="protected")
            {
                globalVar.ittr++;
                return true;
            }
            else
            {
                return true;
            }
        }//written

        public bool swSt()
        {
            if (globalVar.lexTokens[globalVar.ittr]=="switch")
            {
                globalVar.ittr++;
                if (globalVar.lexTokens[globalVar.ittr]=="(")
                {
                    globalVar.ittr++;
                    if (oE())
                    {
                        if (globalVar.lexTokens[globalVar.ittr] == ")")
                        {
                            globalVar.ittr++;
                            if (globalVar.lexTokens[globalVar.ittr] == "{")
                            {
                                globalVar.ittr++;
                                if (caseSt())
                                {
                                    if (defSt())
                                    {
                                        if (globalVar.lexTokens[globalVar.ittr] == "}")
                                        {
                                            globalVar.ittr++;
                                            return true;
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                        
                                    }
                                    else
                                    {
                                        return false;
                                    }


                                }
                                else
                                {
                                    return false;
                                }

                            }
                            else
                            {
                                return false;
                            }

                        }
                        else
                        {
                            return false;
                        }

                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }

        }

        public bool defSt()
        {
            if (globalVar.lexTokens[globalVar.ittr]=="default")
            {
                globalVar.ittr++;
                if (globalVar.lexTokens[globalVar.ittr] == ":")
                {
                    globalVar.ittr++;
                    if (globalVar.lexTokens[globalVar.ittr] == "{")
                    {
                        globalVar.ittr++;
                        if (mST())
                        {
                            if (globalVar.lexTokens[globalVar.ittr] == "break")
                            {
                                globalVar.ittr++;
                                if (globalVar.lexTokens[globalVar.ittr] == ";")
                                {
                                    globalVar.ittr++;
                                    if (globalVar.lexTokens[globalVar.ittr] == "}")
                                    {
                                        globalVar.ittr++;
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool caseSt()
        {
            if (globalVar.lexTokens[globalVar.ittr]=="case")
            {
                globalVar.ittr++;
                if (idConst())
                {
                    if (globalVar.lexTokens[globalVar.ittr] == ":")
                    {
                        globalVar.ittr++;
                        if (globalVar.lexTokens[globalVar.ittr] == "{")
                        {
                            globalVar.ittr++;
                            if (mST())
                            {
                                if (globalVar.lexTokens[globalVar.ittr] == "break")
                                {
                                    globalVar.ittr++;
                                    if (globalVar.lexTokens[globalVar.ittr] == ";")
                                    {
                                        globalVar.ittr++;
                                        if (globalVar.lexTokens[globalVar.ittr] == "}")
                                        {

                                            globalVar.ittr++;
                                            if (caseSt())
                                            {
                                                return true;
                                            }
                                            else
                                            {
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public bool ifSt()
        {
            if (globalVar.lexTokens[globalVar.ittr]=="if")
            {
                globalVar.ittr++;
                if (globalVar.lexTokens[globalVar.ittr] == "(")
                {
                    globalVar.ittr++;
                    if (oE())
                    {
                        if (globalVar.lexTokens[globalVar.ittr] == ")")
                        {
                            globalVar.ittr++;
                            if (body())
                            {
                                if (elseSt())
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool elseSt()
        {
            if (globalVar.lexTokens[globalVar.ittr]=="else")
            {
                globalVar.ittr++;
                if (body())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public bool incdecSt()
        {
            if (id())
            {
                if (_incdec())
                {
                    if (globalVar.lexTokens[globalVar.ittr] == ";")
                    {
                        globalVar.ittr++;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (_incdec())
            {
                if (id())
                {
                    if (globalVar.lexTokens[globalVar.ittr] == ";")
                    {
                        globalVar.ittr++;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool _incdec()
        {
            if (globalVar.tokenType[globalVar.ittr]=="INC"|| globalVar.tokenType[globalVar.ittr] == "DEC")
            {
                globalVar.ittr++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool whileSt()
        {
            if (globalVar.lexTokens[globalVar.ittr] == "while")
            {
                globalVar.ittr++;
                if (globalVar.lexTokens[globalVar.ittr] == "(")
                {
                    globalVar.ittr++;
                    if (oE())
                    {
                        if (globalVar.lexTokens[globalVar.ittr] == ")")
                        {
                            globalVar.ittr++;
                            if (body())
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }

                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }
    }

    class symanticAnalyser
    {

    }
}
