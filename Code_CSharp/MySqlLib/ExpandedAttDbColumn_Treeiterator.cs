using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alivever.com.MySqlLib
{
    /// <summary>
    /// List[CExpandedAttDbColumn] Tree 的迭代器。
    /// 用于LoadFromDb<T>中，顺序迭代类中的每一个元素。
    /// 当遇到ExpandedAtt ！= null的时候，直接展开这个属性，并继续从这个属性中的第一个属性开始迭代。
    /// 当一个对象的全部属性迭代完毕以后，就返回null表示结束。调用者可以再次直行Restar来重头进行下一次迭代。
    /// </summary>
    public class CExpandedAttDbColumn_TreeIterator
    {
        protected List<CExpandedAttDbColumn> AttTree;

        /// <summary>
        /// 当前迭代到树的每一层的序号,其中的数字表示下一次应该被迭代的对象序号。
        /// </summary>
        protected List<Int32> NextSteps = new List<Int32>();

        //protected Int32 NextSetp;

        //protected Int32 NextIndex ;

        /// <summary>
        /// StepNull 表示没有。 如果全部为StepNull，则表示迭代完毕。需要Reset后才能重新迭代。
        /// </summary>
        protected const Int32 StepNull = -1;

        public CExpandedAttDbColumn_TreeIterator(List<CExpandedAttDbColumn> _AttTree)
        {
            this.AttTree = _AttTree;

            this.Restar();
        }//CExpandedAttDbColumn_Treeiterator()

        public void Restar()
        {
            //for (Int32 i = 0; i < this.CrrSteps.Length; i++)
            //    this.CrrSteps[i] = StepNull;
            this.NextSteps.Clear();
            this.NextSteps.Add( 0 );

            //NextSetp = 0;
            //NextIndex = 0;

        }//Reset()

        /// <summary>
        /// 返回 下一个 Col的 上层调用序列。
        /// 最后一个Col才是真正的下一个节点。
        /// 他的前面是上层节点。也就是有可能需要被重新New()出来的节点。
        /// </summary>
        /// <returns></returns>
        public CExpandedAttDbColumn GetNext(out List<CExpandedAttDbColumn> _parents)
        {
            //////倒序迭代CrrSteps， 遇到第一个非StepNull 节点后进行执行。
            ///如果this.CrrSteps[0]==StepNull 则表示已经没有可以迭代的内容了，返回null。
            ///收尾1、将当前层级的节点数+1. 
            ///    2.如果当前层级没有后续节点，则将当前层级=StepNull，然后将下一个层级加一。

            _parents = new List<CExpandedAttDbColumn>();

            //bool isAllSetNull = true;
            //foreach (Int32 a in this.NextSteps)
            //    if (a == StepNull)
            //        isAllSetNull = false;


            //如果已经迭代到了结尾，就直接返回nll
            if (this.NextSteps == null || this.NextSteps.Count ==0)
            {
                return null;
            }

            //2. 找到对应的tree节点对象
            //Int32 nxSetp, nxIndex;
            List<Int32> crrSteps = new List<Int32>();
            List<Int32> nxSteps;
            crrSteps.Add(0);
            CExpandedAttDbColumn rstCol = 树状迭代_找到节点(
                //this.NextSetp, this.NextIndex,
                this.NextSteps,
                this.AttTree,
                crrSteps, 
                _parents, 
                out nxSteps);

            if (rstCol == null)
            {
                string nextSetpStr = string.Empty;
                foreach (int crrItem in this.NextSteps)
                    nextSetpStr += $"{crrItem}, ";

                throw new Exception($"class {this.GetType().Name}.GetNext()->树状迭代_找到节点()出错：没有找到树种的某个节点[{nextSetpStr}]");
            }

            //3. 收尾
            this.NextSteps = nxSteps;
            //this.NextIndex = nxIndex;

            return rstCol;
        }//GetNext()

        /// <summary>
        /// 每次迭代都仅循环当前深度的全部col。如果某个col需要向下层展开，则引入迭代。
        /// </summary>
        /// <param name="rstStep"></param>
        /// <param name="rstIndex"></param>
        /// <param name="crrRow"></param>
        /// <param name="rstStep"></param>
        /// <param name="rstIndex"></param>
        /// <returns></returns>
        protected static CExpandedAttDbColumn 树状迭代_找到节点(
            //Int32 rstStep,
            //Int32 rstIndex,
            List<Int32> tgtSteps,
            List<CExpandedAttDbColumn> crrRow,
            List<Int32> crrSteps,
            List<CExpandedAttDbColumn> _parents,
            out List<Int32> nextSteps)
        //protected static CExpandedAttDbColumn 树状迭代_找到节点(
        //    Int32 rstStep,
        //    Int32 rstIndex,
        //    List<CExpandedAttDbColumn> crrRow,
        //    Int32 crrStep,
        //    List<CExpandedAttDbColumn> _parents,
        //    out Int32 nxSetp,
        //    out Int32 nxIndex)
        {
            //如果最终要寻找的层级比当前层级浅，则直接返回空。只有期待的层级等于或大于当前层级时，继续迭代才是有意义的。
            //nxSetp = StepNull;
            //nxIndex = StepNull;
            nextSteps = null;

            //如果没有目标步骤了，就代表迭代完毕，返回null
            if (tgtSteps == null || tgtSteps.Count == 0)
                return null;

            //如果当前深度大于目标深度，则说明肯定不在当前深度中
            if (tgtSteps.Count < crrSteps.Count)
            {
                throw new Exception("树状迭代_找到节点().不应到达的逻辑。这个逻辑应该在调用方判断而不进入");
            }
            //List<CExpandedAttDbColumn> crrRow;
            Int32 crrStep = crrSteps.Count-1;
             

            int rstStep = tgtSteps.Count - 1;
            int rstIndex = tgtSteps[rstStep];

            //深度优先算。
            //List<CExpandedAttDbColumn> needExpendCols = new List<CExpandedAttDbColumn>();
            for (Int32 lvIndex= tgtSteps[crrStep]; lvIndex < crrRow.Count; lvIndex++)
            {

                crrSteps[crrSteps.Count - 1] = lvIndex;

                //如果当前坐标等于期待坐标，就返回
                if (crrStep == rstStep && lvIndex == rstIndex)
                {
                    //如果本层还有可用的NextStep
                    if (lvIndex + 1 < crrRow.Count)
                    {
                        //复制crrSteps到nextSteps
                        nextSteps = new List<int>();
                        foreach (int crrItem in crrSteps)
                            nextSteps.Add(crrItem);

                        //将下一个步骤的 坐标向后移动1格
                        nextSteps[nextSteps.Count - 1] = lvIndex + 1;

                        //如果 本层中下一个可用的NextStep可向下展开，就把迭代深度增加1层
                        if (crrRow[lvIndex + 1].ExpandedMemberTypes != null && crrRow[lvIndex + 1].ExpandedMemberTypes.Count != 0)
                            nextSteps.Add(0);
                    }//if 如果本层还有可用的NextStep

                    return crrRow[lvIndex];
                }//if (crrStep == rstStep && lvIndex == rstIndex)

                //如果当前节点需要展开，就向下层展开
                if (crrRow[lvIndex].ExpandedMemberTypes != null && crrRow[lvIndex].ExpandedMemberTypes.Count != 0)
                {
                    //if (crrStep + 1 > rstStep)
                    //    continue;
                    if (tgtSteps.Count-1 < crrStep + 1)
                        continue;

                    crrSteps.Add(0);
                    CExpandedAttDbColumn rstCol = 树状迭代_找到节点(tgtSteps,
                        crrRow[lvIndex].ExpandedMemberTypes,
                        crrSteps,
                        _parents,
                        out nextSteps);



                    // 如果下层找到了结果，就返回
                    if (rstCol != null)
                    {
                        _parents.Add(crrRow[lvIndex]);

                       //把迭代深度退回
                       crrSteps.RemoveAt(crrSteps.Count - 1);

                        //如果下一层次没有找到可用的NextStep，并且本层有可用的NextStep
                        if ((nextSteps == null || nextSteps.Count ==0) && lvIndex + 1 < crrRow.Count)
                        {

                            //复制crrSteps到nextSteps
                            nextSteps = new List<int>();
                            foreach (int crrItem in crrSteps)
                                nextSteps.Add(crrItem);

                            //将下一个步骤的 坐标向后移动1格
                            nextSteps[nextSteps.Count - 1] = lvIndex + 1;

                            //如果 本层中下一个可用的NextStep可向下展开，就把迭代深度增加1层
                            if (crrRow[lvIndex + 1].ExpandedMemberTypes != null && crrRow[lvIndex + 1].ExpandedMemberTypes.Count != 0)
                                nextSteps.Add(0);

                            ////如果 本层中下一个可用的NextStep不可向下展开，就把本层可用数字+1
                            //if (crrRow[lvIndex + 1].ExpandedMemberTypes == null || crrRow[lvIndex + 1].ExpandedMemberTypes.Count == 0)
                            //{
                            //    crrSteps[crrSteps.Count - 1] = lvIndex + 1;
                            //}
                            //else
                            //{
                            //    crrSteps[crrSteps.Count - 1] = lvIndex + 1;
                            //    crrSteps.Add(0);
                            //}
                        }

                        return rstCol;
                    }
                    continue;
                }//if 如果当前节点需要展开，就向下层展开
            }//for (Int32 i=0; i < CrrSteps.Length; i++)


            return null;
        }//树状迭代_找到节点()



    }// class CExpandedAttDbColumn_Treeiterator
}//namespace
