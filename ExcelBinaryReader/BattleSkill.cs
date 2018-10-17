using System.Collections.Generic;
namespace BinaryConfig
{
    class BattleSkill : IBinaryData
    {
        /// <summary>
        /// 唯一编号
        /// <summary>
        private int mid;
        public int id
        {
            get{ return mid;}
        }
        /// <summary>
        /// 名称
        /// <summary>
        private string mname;
        public string name
        {
            get{ return mname;}
        }
        /// <summary>
        /// 显示等级
        /// <summary>
        private int mskill_level_shoe;
        public int skill_level_shoe
        {
            get{ return mskill_level_shoe;}
        }
        /// <summary>
        /// 技能描述
        /// <summary>
        private string mdesc;
        public string desc
        {
            get{ return mdesc;}
        }
        /// <summary>
        /// 升级效果描述
        /// <summary>
        private string mlvup_desc;
        public string lvup_desc
        {
            get{ return mlvup_desc;}
        }
        /// <summary>
        /// 图标
        /// <summary>
        private string micon;
        public string icon
        {
            get{ return micon;}
        }
        /// <summary>
        /// 技能总时长 
        /// <summary>
        private int mduration;
        public int duration
        {
            get{ return mduration;}
        }
        /// <summary>
        /// 自动战斗时，主动技能施放优先级
        /// <summary>
        private int mskill_priority;
        public int skill_priority
        {
            get{ return mskill_priority;}
        }
        /// <summary>
        /// 技能类型
        /// <summary>
        private int mskill_type;
        public int skill_type
        {
            get{ return mskill_type;}
        }
        /// <summary>
        /// 技能cd
        /// <summary>
        private int mskill_cd;
        public int skill_cd
        {
            get{ return mskill_cd;}
        }
        /// <summary>
        /// 技能初始CD
        /// <summary>
        private int minit_cd;
        public int init_cd
        {
            get{ return minit_cd;}
        }
        /// <summary>
        /// 怒气消耗
        /// <summary>
        private int menergy;
        public int energy
        {
            get{ return menergy;}
        }
        /// <summary>
        /// 生命消耗百分比
        /// <summary>
        private int mhp_cost_rate;
        public int hp_cost_rate
        {
            get{ return mhp_cost_rate;}
        }
        /// <summary>
        /// 技能表现id
        /// <summary>
        private int mskillex_id;
        public int skillex_id
        {
            get{ return mskillex_id;}
        }
        /// <summary>
        /// 大招表现ID
        /// <summary>
        private int msuper_skillex_id;
        public int super_skillex_id
        {
            get{ return msuper_skillex_id;}
        }
        /// <summary>
        /// 目标阵营
        /// <summary>
        private int matk_side;
        public int atk_side
        {
            get{ return matk_side;}
        }
        /// <summary>
        /// 选择目标类型
        /// <summary>
        private int mtarget_type;
        public int target_type
        {
            get{ return mtarget_type;}
        }
        /// <summary>
        /// 如果目标类型为部分，该字段指定目标个数上限
        /// <summary>
        private int mtarget_count;
        public int target_count
        {
            get{ return mtarget_count;}
        }
        /// <summary>
        /// 目标血量百分比要求
        /// <summary>
        private int mtarget_hp;
        public int target_hp
        {
            get{ return mtarget_hp;}
        }
        /// <summary>
        /// 效果作用类型组
        /// <summary>
        private int[] meffect_target_types;
        public int[] effect_target_types
        {
            get{ return meffect_target_types;}
        }
        /// <summary>
        /// 技能效果组ID
        /// <summary>
        private int[] mskilleffect_id;
        public int[] skilleffect_id
        {
            get{ return mskilleffect_id;}
        }
        /// <summary>
        /// 技能buff组ID
        /// <summary>
        private int[] mskillbuff_id;
        public int[] skillbuff_id
        {
            get{ return mskillbuff_id;}
        }
        /// <summary>
        /// 技能命中修正值
        /// <summary>
        private int mhit_correct;
        public int hit_correct
        {
            get{ return mhit_correct;}
        }
        /// <summary>
        /// 下一级技能ID
        /// <summary>
        private int mnext_skill;
        public int next_skill
        {
            get{ return mnext_skill;}
        }
        /// <summary>
        /// 觉醒后技能
        /// <summary>
        private int mwake_skill;
        public int wake_skill
        {
            get{ return mwake_skill;}
        }
        /// <summary>
        /// 技能暴击率
        /// <summary>
        private int mskill_crit;
        public int skill_crit
        {
            get{ return mskill_crit;}
        }
        /// <summary>
        /// 技能抗暴击率
        /// <summary>
        private int mskill_anti_crit;
        public int skill_anti_crit
        {
            get{ return mskill_anti_crit;}
        }
        /// <summary>
        /// 初始技能id
        /// <summary>
        private int minitial_skill_id;
        public int initial_skill_id
        {
            get{ return minitial_skill_id;}
        }
        public void Init(BinaryConfigRow row)
        {
            mid = row.GetFieldInfo(0).GetInt();
            mname = row.GetFieldInfo(1).GetString();
            mskill_level_shoe = row.GetFieldInfo(2).GetInt();
            mdesc = row.GetFieldInfo(3).GetString();
            mlvup_desc = row.GetFieldInfo(4).GetString();
            micon = row.GetFieldInfo(5).GetString();
            mduration = row.GetFieldInfo(6).GetInt();
            mskill_priority = row.GetFieldInfo(7).GetInt();
            mskill_type = row.GetFieldInfo(8).GetInt();
            mskill_cd = row.GetFieldInfo(9).GetInt();
            minit_cd = row.GetFieldInfo(10).GetInt();
            menergy = row.GetFieldInfo(11).GetInt();
            mhp_cost_rate = row.GetFieldInfo(12).GetInt();
            mskillex_id = row.GetFieldInfo(13).GetInt();
            msuper_skillex_id = row.GetFieldInfo(14).GetInt();
            matk_side = row.GetFieldInfo(15).GetInt();
            mtarget_type = row.GetFieldInfo(16).GetInt();
            mtarget_count = row.GetFieldInfo(17).GetInt();
            mtarget_hp = row.GetFieldInfo(18).GetInt();
            meffect_target_types = row.GetFieldInfo(19).GetIntList();
            mskilleffect_id = row.GetFieldInfo(20).GetIntList();
            mskillbuff_id = row.GetFieldInfo(21).GetIntList();
            mhit_correct = row.GetFieldInfo(22).GetInt();
            mnext_skill = row.GetFieldInfo(23).GetInt();
            mwake_skill = row.GetFieldInfo(24).GetInt();
            mskill_crit = row.GetFieldInfo(25).GetInt();
            mskill_anti_crit = row.GetFieldInfo(26).GetInt();
            minitial_skill_id = row.GetFieldInfo(27).GetInt();
        }
    }
}

